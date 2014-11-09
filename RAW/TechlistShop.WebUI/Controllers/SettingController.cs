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
    public class SettingController : Controller
    {
        private ISettingRepository repository;

        public SettingController(ISettingRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult Index()
        {
            return View(repository.Settings);
        }

        public ViewResult Edit(int settingId)
        {
            ViewBag.title = "تغییر تنظیمات نمایه";
            Setting setting = repository.Settings
                                .Where(x => x.SettingID == settingId)
                                .FirstOrDefault();
            return View(setting);
        }

        [HttpPost]
        public ActionResult Edit(Setting setting)
        {
            setting.DateModified = DateTime.Now;
            if (ModelState.IsValid)
            {
                repository.SaveSetting(setting);
                TempData["message"] = string.Format("تغییر تنظیمات با موفقیت انجام شد.");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "عملیات با مشکل مواجه شد.");
                return View(setting);
            }

        }
    }
}
