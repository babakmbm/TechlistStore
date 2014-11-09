using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace TechlistShop.Domain.Entities
{
    public class Category
    {
        [HiddenInput(DisplayValue = false)]
        [DisplayName("شماره")]
        [Required]
        public int CategoryID { get; set; }

        [DisplayName("نام")]
        [Required(ErrorMessage = "لطفا نام محصول را وارد کنید.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا توضیحات محصول را وارد کنید.")]
        [DataType(DataType.MultilineText)]
        [DisplayName("توضیحات")]
        public string Description { get; set; }

        public bool Featured { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
