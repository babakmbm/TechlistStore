using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using TechlistShop.Domain.Entities;
using System.Collections.Generic;

namespace TechlistShop.Domain.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int ShipperID { get; set; }
        public int PaymentID { get; set; }
        public DateTime DateOrdered { get; set; }
        public decimal Tax { get; set; }
        public bool Fulfilled { get; set; }
        public bool Canceled { get; set; }
        public bool Paid { get; set; }
        public bool PaymentDate { get; set; }
    }
}
