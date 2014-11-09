using System.Collections.Generic;
using TechlistShop.Domain.Entities;

namespace TechlistShop.WebUI.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<CarouselImage> CarouselImages { get; set; }
        public IEnumerable<DiscountCarouselImage> DiscountCarouselImages { get; set; }
        public IEnumerable<Add> Adds { get; set; }

        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}