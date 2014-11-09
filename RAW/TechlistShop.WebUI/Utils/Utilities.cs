using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechlistShop.WebUI.Models
{
    public class Utilities
    {
        public string Limit(string input, int max)
        {
            if (!String.IsNullOrEmpty(input) && input.Length > max)
            {
                return input.Substring(0, max);
            }
            return input;
        }
    }
}