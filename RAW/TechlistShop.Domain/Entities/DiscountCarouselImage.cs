﻿using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace TechlistShop.Domain.Entities
{
    public class DiscountCarouselImage
    {
        [Required]
        [Key]
        public int ImageId { get; set; }

        [Required]
        [Display(Name = "شماره عکس")]
        public string number { get; set; }

        [Required]
        [Display(Name = "نام عکس")]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "لینک")]
        public string ImageLinkUrl { get; set; }

        [Required]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "فعال؟")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "تاریخ اتمام تخفیف")]
        public DateTime EndDate { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}
