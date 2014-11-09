using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechlistShop.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage="لطفا نام کامل خود را وارد کنید")]
        [DisplayName("نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا آدرس خود را وارد کنید")]
        [DisplayName("آدرس(خط-1)")]
        public string AddLine1 { get; set; }
        [DisplayName("آدرس(خط-2)")]
        public string AddLine2 { get; set; }
        [DisplayName("آدرس(خط-3)")]
        public string AddLine3 { get; set; }

        [Required(ErrorMessage = "لطفا شهر را وارد کنید")]
        [DisplayName("شهر")]
        public string City { get; set; }

        [Required(ErrorMessage = "لطفا استان را وارد کنید")]
        [DisplayName("استان")]
        public string State { get; set; }

        [Required(ErrorMessage = "لطفا کد پستی را وارد کنید")]
        [DisplayName("کد پستی ده رقمی")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "لطفا کشور را وارد کنید")]
        [DisplayName("کشور")]
        public string Country { get; set; }

        [DisplayName("کادو شود")]
        public bool GiftWrap { get; set; }
    }
}