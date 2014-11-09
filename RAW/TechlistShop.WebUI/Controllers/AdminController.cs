using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechlistShop.Domain.Entities;
using TechlistShop.Domain.Abstract;
using TechlistShop.WebUI.Models;

namespace TechlistShop.WebUI.Controllers
{
    [Authorize(Roles="Admins")]
    public class AdminController : Controller
    {
        UsersContext userContext = new UsersContext(); 

        private IProductRepository repository;

        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ProductList()
        {
            return View(repository.Products.OrderBy(x=>x.Category));
        }

        public ActionResult UserManagement()
        {
            return View(userContext.UserProfiles);
        }

        public ViewResult Edit(int productId)
        {
            ViewBag.title = "ویرایش محصول";
            Product product = repository.Products
                                .Where(x => x.ProductID == productId)
                                .FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                if (!repository.SaveProduct(product)) //isNew = false
                {
                    TempData["message"] = string.Format("ویرایش «{0}» با موفقیت انجام شد.", product.Name);
                    return RedirectToAction("ProductList");
                }
                else //isNew = true
                {
                    TempData["message"] = string.Format("«{0}» با موفقیت به پایگاه داده اضافه شد.", product.Name);
                    return RedirectToAction("ProductList");
                }
            }
            else
            {
                ModelState.AddModelError("", "عملیات با مشکل مواجه شد.");
                return View(product);
            }
            
        }

        public ViewResult Create()
        {
            ViewBag.title = "محصول جدید";
            ViewBag.date = DateTime.Now;
            ViewBag.create = true;
            return View("Edit", new Product());
        }

        public ActionResult Delete(int productId)
        {
            Product delProduct = repository.DeleteProduct(productId);

            if (delProduct != null)
            {
                TempData["message"] = string.Format("«{0}» با موفقت از پایگاه داده حذف شد.", delProduct.Name);
            }
            else
            {
                TempData["error"] = "عملیات حذف با مشکل مواجه شد.";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int productId)
        {
            Product product = repository.Products
                    .Where(x => x.ProductID == productId)
                    .FirstOrDefault();
            return View(product);
        }
    }
}