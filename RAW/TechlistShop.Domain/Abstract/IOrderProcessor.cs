using System.Linq;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);

        IQueryable<Order> Orders { get; }

        void SaveOrder(Order order, Cart cart);

        Order DeleteOrder(int OrderID);
    }
}
