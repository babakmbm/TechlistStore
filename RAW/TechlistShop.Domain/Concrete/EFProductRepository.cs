using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechlistShop.Domain.Abstract;
using TechlistShop.Domain.Entities;

namespace TechlistShop.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.Find(productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public void QuantityReducer(Product product, int Quantity)
        {
            Product dbEntry = context.Products.Find(product.ProductID);
            if (dbEntry != null)
            {
                dbEntry.Quantity = dbEntry.Quantity - Quantity;
                if (dbEntry.Quantity <= 0)
                {
                    dbEntry.Availability = false;
                }
                context.SaveChanges();
            }
        }

        public bool SaveProduct(Product product)
        {
            bool isNew;
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
                isNew = true;
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Model = product.Model;
                    dbEntry.Availability = product.Availability;
                    dbEntry.Quantity = product.Quantity;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    if (product.ImageData != null)
                    {
                        dbEntry.ImageData = product.ImageData;
                        dbEntry.ImageMimeType = product.ImageMimeType;
                    }
                }
                isNew = false;
            }
            context.SaveChanges();
            return isNew;
        }
    }
}
