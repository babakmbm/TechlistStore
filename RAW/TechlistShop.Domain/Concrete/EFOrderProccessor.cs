using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TechlistShop.Domain.Abstract;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "b7.hosseini@gmail.com";
        public string MailFromAddress = "b7.hosseini@gmail.com";
        public bool UseSsl = true;
        public string UserName = "b7.hosseini@gmail.com";
        public string Password = "3Obvious";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\techlist_store_emails";
    }

    public class EFOrderProccessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        private EFDbContext context = new EFDbContext();

        public EFOrderProccessor(EmailSettings setting)
        {
            emailSettings = setting;
        }

        public IQueryable<Order> Orders
        {
            get { return context.Orders; }
        }

        public void SaveOrder(Order order, Cart cart)
        {
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            else
            {
                Order dbEntry = context.Orders.Find(order.OrderID);
                if (dbEntry != null)
                {
                    dbEntry.CustomerID = order.CustomerID;
                    dbEntry.ShipperID = order.ShipperID;
                    dbEntry.PaymentID = order.PaymentID;
                    dbEntry.DateOrdered = order.DateOrdered;
                    dbEntry.Tax = order.Tax;
                    dbEntry.Fulfilled = order.Fulfilled;
                    dbEntry.Canceled = order.Canceled;
                    dbEntry.Paid = order.Paid;
                    //dbEntry.PaymentDate = order.PaymentDate;
                }
            }
            context.SaveChanges();
        }

        public Order DeleteOrder(int OrderID)
        {
            Order dbEntry = context.Orders.Find(OrderID);
            if (dbEntry != null)
            {
                context.Orders.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("یک خرید جدید انجام شده است.")
                    .AppendLine("----")
                    .AppendLine("محصولات خریداری شده:");

                foreach (var item in cart.Items)
                {
                    var subTotal = item.Product.Price * item.Quantity;
                    body.AppendFormat("{0} * {1} (به مبلغ: {2:c}", item.Quantity,item.Product.Name ,subTotal);
                    body.AppendLine("\n");
                }
                string gift = null;
                if (shippingInfo.GiftWrap) { gift = "بله"; } else { gift = "خیر"; }
                body.AppendFormat("مبلغ کل فروش: {0:c}", cart.ComputeValue());
                body.AppendLine("\n---");
                body.AppendLine("اطلاعات مشتری:");
                body.AppendLine(shippingInfo.Name);
                body.AppendLine(shippingInfo.AddLine1);
                body.AppendLine(shippingInfo.AddLine2 ?? "");
                body.AppendLine(shippingInfo.AddLine3 ?? "");
                body.AppendLine(shippingInfo.City);
                body.AppendLine(shippingInfo.State ?? "");
                body.AppendLine(shippingInfo.Zip);
                body.AppendLine("---");
                body.AppendFormat("Gift: {0}", gift);
                
                MailMessage mailMessage = new MailMessage(
                            emailSettings.MailFromAddress,
                            emailSettings.MailToAddress,
                            "خرید جدید انجام شده است!",
                            body.ToString());
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
