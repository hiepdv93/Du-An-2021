using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Productgold.Models
{
    public class Cart
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productImg { get; set; }
        public double price { get; set; }
        public int count { get; set; }
        public double total { get; set; }
        public string key { get; set; }
    }
}