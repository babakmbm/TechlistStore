using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TechlistShop.Domain.Entities
{
    public class Add
    {
        [Required]
        [Key]
        public int AddId { get; set; }
        [Required]
        [Display(Name = "نام آگهی")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "لینک آگهی")]
        public string LinkUrl { get; set; }

        [Required]
        [Display(Name = "توضیحات آگهی")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "فعال؟")]
        public bool Active { get; set; }
        
        [Required]
        [Display(Name = "ترتیب")]
        public int Order { get; set; }

        public DateTime DateAdded { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

    }
}
