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
    [Authorize(Roles="Admins")]
    public class AddController : Controller
    {
        private IAddRepository repository;

        public AddController(IAddRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult Index()
        {
            return View(repository.Adds);
        }

        public ActionResult Details(int addId)
        {
            Add add = repository.Adds
                                .Where(x => x.AddId == addId)
                                .FirstOrDefault();

            return View(add);
        }

        public ViewResult Edit(int addId)
        {
            ViewBag.title = "ویرایش آگهی";
            Add add = repository.Adds
                                .Where(x => x.AddId == addId)
                                .FirstOrDefault();
            return View(add);
        }

        [HttpPost]
        public ActionResult Edit(Add add, HttpPostedFileBase image)
        {
            add.DateAdded = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    add.ImageMimeType = image.ContentType;
                    add.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(add.ImageData, 0, image.ContentLength);
                }
                if (!repository.SaveAdd(add)) //isNew = false
                {
                    TempData["message"] = string.Format("ویرایش «{0}» با موفقیت انجام شد.", add.Name);
                    return RedirectToAction("Index");
                }
                else //isNew = true
                {
                    TempData["message"] = string.Format("«{0}» با موفقیت به پایگاه داده اضافه شد.", add.Name);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "عملیات با مشکل مواجه شد.");
                return View(add);
            }

        }

        public ViewResult Create()
        {
            ViewBag.title = "آگهی جدید";
            return View("Edit", new Add());
        }

        public ActionResult Delete(int addId)
        {
            Add delAdd = repository.DeleteAdd(addId);

            if (delAdd != null)
            {
                TempData["message"] = string.Format("«{0}» با موفقت از پایگاه داده حذف شد.", delAdd.Name);
            }
            else
            {
                TempData["error"] = "عملیات حذف با مشکل مواجه شد.";
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public FileContentResult GetImage(int addId)
        {
            Add AImage = repository.Adds.Where(x => x.AddId == addId).FirstOrDefault();

            if (AImage != null)
            {
                return File(AImage.ImageData, AImage.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

    }
}
