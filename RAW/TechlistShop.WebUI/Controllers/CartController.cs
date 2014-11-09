using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechlistShop.Domain.Entities;
using TechlistShop.Domain.Abstract;
using TechlistShop.WebUI.Models;
using System.Web.Security;
using WebMatrix.WebData;

namespace TechlistShop.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        UsersContext usercontext = new UsersContext();

        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            this.repository = repo;
            this.orderProcessor = proc;
        }

        [Authorize]
        public ActionResult ProfileCheck(Cart cart)
        {
            if (cart.Items.Count() == 0)
            {
                ViewBag.emptyFlag = true;
                ViewBag.Message = "هیچ آیتمی در سبد خرید شما وجود ندارد.";
                return View("Index");
            }
            else
            {
                var userId = WebSecurity.GetUserId(User.Identity.Name);
                var userProfile = usercontext.UserProfiles
                                    .Where(x => x.UserId == userId).FirstOrDefault();
                return View(userProfile);
            }
        }

        //[Authorize]
        //public ActionResult Checkout()
        //{
        //    CheckOutViewModel checkOutViewModel = new CheckOutViewModel();
        //    return View(checkOutViewModel);
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult Checkout(Cart cart,CheckOutViewModel CKOVM)
        //{

        //    if (cart.Items.Count() == 0)
        //    {
        //        ModelState.AddModelError("", "متاسفانه آیتمی در سبد خرید شما موجود نیست!");
        //    }
        //    //else
        //    //{
        //    //    CKOVM.order.CustomerID = Customer.UserId;
        //    //    CKOVM.order.DateOrdered = DateTime.Now;
        //    //    CKOVM.order.Fulfilled = false;
        //    //    CKOVM.order.Paid = false;
        //    //    CKOVM.order.PaymentID = 1;
        //    //    CKOVM.order.ShipperID = 1;
        //    //    CKOVM.order.Tax = 0;
        //    //    CKOVM.order.Canceled = false;

        //    //    orderProcessor.SaveOrder(CKOVM.order, cart);
        //    //}

        //    if (ModelState.IsValid)
        //    {
        //        foreach (var c in cart.Items)
        //        {
        //            repository.QuantityReducer(c.Product, c.Quantity);
        //        }
                
        //        orderProcessor.ProcessOrder(cart, CKOVM.ShippingDetails);
        //        cart.Clear();
        //        return View("Completed");
        //    }
        //    else 
        //    {
        //        return View(CKOVM);
        //    }
        //}

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ActionResult Index(Cart cart,string returnUrl)
        {
            CartIndexViewModel cartIndex = new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            if (cartIndex.Cart.Items.Count() != 0)
            {
                ViewBag.emptyFlag = false;
                return View(cartIndex);
            }
            else
            {
                ViewBag.emptyFlag = true;
                ViewBag.Message = "هیچ آیتمی در سبد خرید شما وجود ندارد.";
                return View(cartIndex);
            }
        }

        public RedirectToRouteResult AddToCart(Cart cart,int productId, string returnUrl)
        {
            Product product = repository.Products
                                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                if (product.Availability == true && product.Quantity > 0)
                {
                    cart.AddItem(product, 1);
                }
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl, string quantity)
        {
            Product product = repository.Products
                                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        //WE DONT NEED THIS AFTER CREATING THE CUSTOME MODELBINDER(CartModelBinder)
        //AND REGISTERING IT IN THE "Global.asax.cs"

        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

    }
}
