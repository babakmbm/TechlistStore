using System.Collections.Generic;
using TechlistShop.Domain.Entities;

namespace TechlistShop.WebUI.Models
{
    public class CheckOutViewModel
    {
        public ShippingDetails ShippingDetails { get; set; }
        public Order order { get; set; }
    }
}