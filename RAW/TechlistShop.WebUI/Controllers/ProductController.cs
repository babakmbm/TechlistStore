using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechlistShop.Domain.Abstract;
using TechlistShop.Domain.Entities;
using TechlistShop.WebUI.Models;
using WebMatrix.WebData;

namespace TechlistShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository; 
        private ICarouselImageRepository repositoryI;
        private IDiscountCarouselImageRepository repositoryDI;
        private IAddRepository repositoryA;
        private ISettingRepository repositoryS;
        UsersContext usercontext = new UsersContext();

        public ProductController(IProductRepository productRepository, 
                                 ICarouselImageRepository carouselRepositoryI,
                                 IDiscountCarouselImageRepository repositoryDI, 
                                 IAddRepository addRepositoryA , 
                                 ISettingRepository settingRepositoryS)
        {
            this.repository = productRepository;
            this.repositoryI = carouselRepositoryI;
            this.repositoryDI = repositoryDI;
            this.repositoryA = addRepositoryA;
            this.repositoryS = settingRepositoryS;
        }

        public int CarouselPicNo;
        public int DISCarouselPicNo;
        public int AddNo;
        public int PageSize;

        public ViewResult Index(string category, int pageNumber = 1)
        {
            CarouselPicNo = repositoryS.Settings.Select(s => s.MCNOPIC).FirstOrDefault();
            DISCarouselPicNo = repositoryS.Settings.Select(s => s.DISNOPIC).FirstOrDefault();
            AddNo = repositoryS.Settings.Select(s => s.NOA).FirstOrDefault();
            ViewBag.SlideToNumber = 0;
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .Where(p => p.Availability == true)
                .OrderByDescending(p => p.ProductID)
                .Skip((pageNumber - 1) * 4)
                .Take(4),
                CarouselImages = repositoryI.CarouselImages
                                .Where(x=> x.Active == true)
                                .OrderBy(x => x.ImageId)
                                .Skip(Math.Max(0, repositoryI.CarouselImages.Count() - CarouselPicNo))
                                .Take(CarouselPicNo),
                DiscountCarouselImages = repositoryDI.DiscountCarouselImages
                                .Where(x=> x.EndDate > DateTime.Now)
                                .OrderBy(x => x.ImageId)
                                .Take(DISCarouselPicNo),
                Adds = repositoryA.Adds
                        .Where(x => x.Active == true)
                        .OrderBy(x => x.AddId)
                        .Skip(Math.Max(0, repositoryA.Adds.Where(x => x.Active == true).Count() - AddNo))
                        .Take(AddNo),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = 4,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public ViewResult List(string category, int pageNumber = 1)
        {
            CarouselPicNo = repositoryS.Settings.Select(s => s.MCNOPIC).FirstOrDefault();
            AddNo = repositoryS.Settings.Select(s => s.NOA).FirstOrDefault();
            PageSize = repositoryS.Settings.Select(s => s.LPS).FirstOrDefault();
            ViewBag.SlideToNumber = 0;
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderByDescending(p => p.ProductID)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize),

                CarouselImages = repositoryI.CarouselImages
                                .Where(x => x.Active == true)
                                .OrderBy(x=>x.ImageId)
                                .Skip(Math.Max(0, repositoryI.CarouselImages.Count() - CarouselPicNo))
                                .Take(CarouselPicNo),
                Adds = repositoryA.Adds
                        .Where(x => x.Active == true)
                        .OrderBy(x => x.AddId)
                        .Skip(Math.Max(0, repositoryA.Adds.Where(x => x.Active == true).Count() - AddNo))
                        .Take(AddNo),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public ActionResult Details(int productID)
        {
            var product = repository.Products
                                    .Where(x => x.ProductID == productID)
                                    .FirstOrDefault();

            return View(product);
        }

        public ViewResult Search(string q, int pageNumber = 1)
        {
            //TODO: Make an Error System for invalid queries.
            ViewBag.SearchQuery = q;
            PageSize = 6;
            ProductListViewModel model = new ProductListViewModel()
            {

                Products = repository.Products
                    .Where(p => p.Name.Contains(q) || p.Description.Contains(q))
                    .OrderBy(p => p.ProductID)
                    .Skip((pageNumber - 1) * PageSize)
                    .Take(PageSize),
                CarouselImages = repositoryI.CarouselImages.OrderBy(x=> x.ImageId),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products
                                .Where(p => p.Name.Contains(q) || p.Description.Contains(q)).Count()
                },
                CurrentCategory = null
            };
            return View(model);
        }

        public FileContentResult GetImage(int productID)
        {
            Product prod = repository.Products.Where(p => p.ProductID == productID).FirstOrDefault();
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
