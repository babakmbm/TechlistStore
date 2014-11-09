using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using TechlistShop.Domain.Entities;

namespace TechlistShop.WebUI.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("EFDbContext")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "شماره کاربر")]
        public int UserId { get; set; }

        [Display(Name="نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "نام خود را وارد کنید")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی خود را وارد کنید")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "پست الکترونیکی")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "آدرس خود را وارد کنید")]
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Required(ErrorMessage = "شهر خود را وارد کنید")]
        [Display(Name = "شهر")]
        public string City { get; set; }

        [Required(ErrorMessage = "استان خود را وارد کنید")]
        [Display(Name = "استان")]
        public string State { get; set; }

        [Required(ErrorMessage = "کد پستی 10 رقمی خود را وارد کنید")]
        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "کشور خود را وارد کنید")]
        [Display(Name = "کشور")]
        public string Country { get; set; }

        [Required(ErrorMessage = "تلفن ثابت خود را وارد کنید")]
        [Display(Name = "تلفن ثابت")]
        public string LandLine { get; set; }

        [Required(ErrorMessage = "تلفن همراه خود را وارد کنید")]
        [Display(Name = "تلفن همراه")]
        public string CellPhone { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "رمز عبور قدیمی را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور قدیمی")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "رمز عبور جدید را وارد کنید")]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و تکرار آن همخوانی ندارند")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage="نام کاربری را وارد کنید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "نام خود را وارد کنید")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی خود را وارد کنید")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "پست الکترونیکی خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "یک ایمیل واقعی وارد کنید")]
        [Display(Name = "آدرس ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن همخوانی ندارند")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "آدرس خود را وارد کنید")]
        [Display(Name = "آدرس 1")]
        public string Address1 { get; set; }

        [Display(Name = "آدرس 2")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "شهر خود را وارد کنید")]
        [Display(Name = "شهر")]
        public string City { get; set; }

        [Required(ErrorMessage = "استان خود را وارد کنید")]
        [Display(Name = "استان")]
        public string State { get; set; }

        [Required(ErrorMessage = "کد پستی 10 رقمی خود را وارد کنید")]
        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "کشور خود را وارد کنید")]
        [Display(Name = "کشور")]
        public string Country { get; set; }

        [Required(ErrorMessage = "تلفن ثابت خود را وارد کنید")]
        [Display(Name = "تلفن ثابت")]
        public string LandLine { get; set; }

        [Required(ErrorMessage = "تلفن همراه خود را وارد کنید")]
        [Display(Name = "تلفن همراه")]
        public string CellPhone { get; set; }

    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
