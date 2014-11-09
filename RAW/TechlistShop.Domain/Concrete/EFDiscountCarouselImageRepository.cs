using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechlistShop.Domain.Abstract;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Concrete
{
    public class EFDiscountCarouselImageRepository : IDiscountCarouselImageRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<DiscountCarouselImage> DiscountCarouselImages
        {
            get { return context.DiscountCarouselImages; }
        }

        public DiscountCarouselImage DeleteImage(int imageId)
        {
            DiscountCarouselImage dbEntry = context.DiscountCarouselImages.Find(imageId);

            if (dbEntry != null)
            {
                context.DiscountCarouselImages.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public bool SaveImage(DiscountCarouselImage discountCarouselImage)
        {
            bool isNew;
            if (discountCarouselImage.ImageId == 0)
            {
                context.DiscountCarouselImages.Add(discountCarouselImage);
                isNew = true;
            }
            else
            {
                DiscountCarouselImage dbEntry = context.DiscountCarouselImages.Find(discountCarouselImage.ImageId);
                if (dbEntry != null)
                {
                    dbEntry.Caption = discountCarouselImage.Caption;
                    dbEntry.number = discountCarouselImage.number;
                    dbEntry.ImageLinkUrl = discountCarouselImage.ImageLinkUrl;
                    dbEntry.Description = discountCarouselImage.Description;
                    dbEntry.Active = discountCarouselImage.Active;
                    dbEntry.EndDate = discountCarouselImage.EndDate;
                    

                    if (discountCarouselImage.ImageData != null)
                    {
                        dbEntry.ImageData = discountCarouselImage.ImageData;
                        dbEntry.ImageMimeType = discountCarouselImage.ImageMimeType;
                    }
                }
                isNew = false;
            }
            context.SaveChanges();
            return isNew;
        }
    }
}
