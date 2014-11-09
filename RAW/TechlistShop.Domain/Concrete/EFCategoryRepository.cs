//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TechlistShop.Domain.Abstract;
//using TechlistShop.Domain.Entities;

//namespace TechlistShop.Domain.Concrete
//{
//    public class EFCategoryRepository : ICategoryRepository
//    {
//        private EFDbContext context = new EFDbContext();

//        public IQueryable<Category> Categories
//        {
//            get { return context.Categories; }
//        }
//    }
//}
