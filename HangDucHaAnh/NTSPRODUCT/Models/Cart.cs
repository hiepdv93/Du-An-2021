using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTSPRODUCT.Models
{
    public class Cart
    {
        public string productId { get; set; }
        public string productName { get; set; }
        public string productImg { get; set; }
        public int price { get; set; }
        public int count { get; set; }
        public int total { get; set; }
        public string key { get; set; }
    }
}