using System.Linq;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Abstract
{
    public interface ICarouselImageRepository
    {
        IQueryable<CarouselImage> CarouselImages { get; }

        bool SaveImage(CarouselImage carouselImage);

        CarouselImage DeleteImage(int imageId);
    }
}
