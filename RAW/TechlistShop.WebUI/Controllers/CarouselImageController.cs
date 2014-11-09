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
    public class CarouselImageController : Controller
    {
        private ICarouselImageRepository repository;

        public CarouselImageController(ICarouselImageRepository carouselRepository)
        {
            this.repository = carouselRepository;
        }

        public ActionResult Index()
        {
            return View(repository.CarouselImages);
        }

        public ActionResult Details(int imageId)
        {
            CarouselImage image = repository.CarouselImages
                                    .Where(x => x.ImageId == imageId)
                                    .FirstOrDefault();

            return View(image);
        }

        public ViewResult Edit(int imageId)
        {
            ViewBag.title = "ویرایش عکس";
            CarouselImage carouselImage = repository.CarouselImages
                                .Where(x => x.ImageId == imageId)
                                .FirstOrDefault();
            return View(carouselImage);
        }

        [HttpPost]
        public ActionResult Edit(CarouselImage carouselImage, HttpPostedFileBase image)
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
            return View("Edit", new CarouselImage());
        }

        public ActionResult Delete(int imageId)
        {
            CarouselImage delImage = repository.DeleteImage(imageId);

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
        public FileContentResult GetImage(int imageId)
        {
            CarouselImage Cmage = repository.CarouselImages.Where(p => p.ImageId == imageId).FirstOrDefault();
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
