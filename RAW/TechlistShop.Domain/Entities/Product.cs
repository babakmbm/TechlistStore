using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechlistShop.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        [DisplayName("شماره")]
        [Required]
        public int ProductID { get; set; }

        [DisplayName("نام")]
        [Required(ErrorMessage = "لطفا نام محصول را وارد کنید.")]
        public string Name { get; set; }

        [DisplayName("مدل")]
        [Required(ErrorMessage = "لطفا مدل محصول را وارد کنید.")]
        public string Model { get; set; }

        [DisplayName("وضعیت")]
        [Required]
        public bool Availability { get; set; }

        [DisplayName("تعداد موجودی")]
        [Required(ErrorMessage = "لطفا تعداد موجودی محصول را وارد کنید.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "لطفا توضیحات محصول را وارد کنید.")]
        [DataType(DataType.MultilineText)]
        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [Required(ErrorMessage = "لطفا قیمت این محصول را وارد کنید.")]
        [DisplayName("قیمت")]
        [Range(0.01, double.MaxValue, ErrorMessage = "قیمت محصول باید یک عدد مثبت باشد.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "لطفا دسته بندی محصول را مشخص کنید.")]
        [DisplayName("دسته بندی")]
        public string Category { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [DisplayName("تاریخ ایجاد")]
        public DateTime DateAdded { get; set; }

        //public DateTime DateModified { get; set; }

        //public DateTime DateAvailable { get; set; }



        //public int CategoryID { get; set; }

        //public Category Category { get; set; }
        //public string SKU { get; set; }
        //public string SupplierProductID { get; set; }

        //public int? SupplierID { get; set; }
        //public int? QuantityPerUnit { get; set; }
        //public decimal? MSRP { get; set; }
        //public string AvailableSizes { get; set; }
        //public string AvailableColors { get; set; }
        //public int? SizeID { get; set; }
        //public int? ColorID { get; set; }
        //public decimal? Discount { get; set; }
        //public float? UnitWeight { get; set; }
        //public int? UnitsInStock { get; set; }
        //public int? UnitsOnOrder { get; set; }
        //public int? ReorderLevel { get; set; }
        //public bool? ProductAvailable { get; set; }
        //public bool? DiscountAvailable { get; set; }
        //public bool? CurrentOrder { get; set; }
        //public int? Ranking { get; set; }
    }
}

