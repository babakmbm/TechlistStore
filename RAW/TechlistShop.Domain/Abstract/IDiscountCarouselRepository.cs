using System.Linq;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Abstract
{
    public interface IDiscountCarouselImageRepository
    {
        IQueryable<DiscountCarouselImage> DiscountCarouselImages { get; }

        bool SaveImage(DiscountCarouselImage carouselImage);

        DiscountCarouselImage DeleteImage(int imageId);
    }
}
