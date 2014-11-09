using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechlistShop.Domain.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string Type { get; set; }
        public bool Allowed { get; set; }
    }
}
