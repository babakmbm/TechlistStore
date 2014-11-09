using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechlistShop.Domain.Abstract;
using TechlistShop.Domain.Entities;
using TechlistShop.WebUI.Models;

namespace TechlistShop.WebUI.Controllers
{
    [Authorize(Roles = "Admins")]
    public class DiscountCarouselImageController : Controller
    {
        private IDiscountCarouselImageRepository repository;

        public DiscountCarouselImageController(IDiscountCarouselImageRepository carouselRepository)
        {
            this.repository = carouselRepository;
        }

        public ActionResult Index()
        {
            return View(repository.DiscountCarouselImages);
        }

        public ActionResult Details(int imageId)
        {
            DiscountCarouselImage image = repository.DiscountCarouselImages
                                    .Where(x => x.ImageId == imageId)
                                    .FirstOrDefault();

            return View(image);
        }

        public ViewResult Edit(int imageId)
        {
            ViewBag.title = "ویرایش عکس";
            DiscountCarouselImage carouselImage = repository.DiscountCarouselImages
                                .Where(x => x.ImageId == imageId)
                                .FirstOrDefault();
            return View(carouselImage);
        }

        [HttpPost]
        public ActionResult Edit(DiscountCarouselImage carouselImage, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    carouselImage.ImageMimeType = image.ContentType;
                    carouselImage.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(carouselImage.ImageData, 0, image.ContentLength);
                }
                if (!repository.SaveImage(carouselImage)) //isNew = false
                {
                    TempData["message"] = string.Format("ویرایش «{0}» با موفقیت انجام شد.", carouselImage.Caption);
                    return RedirectToAction("Index");
                }
                else //isNew = true
                {
                    TempData["message"] = string.Format("«{0}» با موفقیت به پایگاه داده اضافه شد.", carouselImage.Caption);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "عملیات با مشکل مواجه شد.");
                return View(carouselImage);
            }

        }

        public ViewResult Create()
        {
            ViewBag.title = "عکس جدید";
            return View("Edit", new DiscountCarouselImage());
        }

        public ActionResult Delete(int imageId)
        {
            DiscountCarouselImage delImage = repository.DeleteImage(imageId);

            if (delImage != null)
            {
                TempData["message"] = string.Format("«{0}» با موفقت از پایگاه داده حذف شد.", delImage.Caption);
            }
            else
            {
                TempData["error"] = "عملیات حذف با مشکل مواجه شد.";
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult DeleteDiscount(int imageId)
        {
            DiscountCarouselImage delImage = repository.DeleteImage(imageId);
            return RedirectToAction("Index","Product");
        }

        [AllowAnonymous]
        public FileContentResult GetImage(int imageId)
        {
            DiscountCarouselImage Cmage = repository.DiscountCarouselImages
                                            .Where(p => p.ImageId == imageId)
                                            .FirstOrDefault();
            if (Cmage != null)
            {
                return File(Cmage.ImageData, Cmage.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
