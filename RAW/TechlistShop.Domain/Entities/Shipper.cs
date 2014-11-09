using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechlistShop.Domain.Entities
{
    public class Shipper
    {
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}
