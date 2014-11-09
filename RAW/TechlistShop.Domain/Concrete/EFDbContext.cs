using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechlistShop.Domain.Entities;
using System.Data.Entity;

namespace TechlistShop.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CarouselImage> CarouselImages { get; set; }
        public DbSet<DiscountCarouselImage> DiscountCarouselImages { get; set; }
        public DbSet<Add> Adds { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
