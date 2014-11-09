using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TechlistShop.Domain.Entities
{
    public class Setting
    {
        public char integritykeeper { get; set; }

        public int SettingID { get; set; }

        [Display(Name="تعداد عکسها در کروسل")]
        [Required(ErrorMessage = "تعداد عکسهای کروسل را وارد کنید. حداقل 1 عکس")]
        public int MCNOPIC { get; set; }

        [Display(Name = "تعداد عکسها در کروسل تخفیف")]
        [Required(ErrorMessage = "تعداد عکسهای کروسل را وارد کنید. حداقل 1 عکس")]
        public int DISNOPIC { get; set; }

        [Display(Name="تعداد آگهی های ستون کناری")]
        public int NOA { get; set; }

        [Display(Name = "تعداد محصولات در صفحه لیست")]
        public int LPS { get; set; }

        [Display(Name = "آخرین تغییر در تنظیمات")]
        public DateTime DateModified { get; set; }

        [Display(Name="توضیحات برای تنظیمات")]
        public string Description { get; set; }
    }
}
