using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechlistShop.Domain.Abstract;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Concrete
{
    public class EFCarouselImageRepository : ICarouselImageRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<CarouselImage> CarouselImages
        {
            get { return context.CarouselImages; }
        }

        public CarouselImage DeleteImage(int imageId)
        {
            CarouselImage dbEntry = context.CarouselImages.Find(imageId);

            if (dbEntry != null)
            {
                context.CarouselImages.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public bool SaveImage(CarouselImage carouselImage)
        {
            bool isNew;
            if (carouselImage.ImageId == 0)
            {
                context.CarouselImages.Add(carouselImage);
                isNew = true;
            }
            else
            {
                CarouselImage dbEntry = context.CarouselImages.Find(carouselImage.ImageId);
                if (dbEntry != null)
                {
                    dbEntry.Caption = carouselImage.Caption;
                    dbEntry.number = carouselImage.number;
                    dbEntry.ImageLinkUrl = carouselImage.ImageLinkUrl;
                    dbEntry.Description = carouselImage.Description;
                    dbEntry.Active = carouselImage.Active;

                    if (carouselImage.ImageData != null)
                    {
                        dbEntry.ImageData = carouselImage.ImageData;
                        dbEntry.ImageMimeType = carouselImage.ImageMimeType;
                    }
                }
                isNew = false;
            }
            context.SaveChanges();
            return isNew;
        }
    }
}
