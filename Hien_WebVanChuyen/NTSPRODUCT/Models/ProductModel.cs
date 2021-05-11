using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTSPRODUCT.Models
{
    public class ProductModel
    {
        public Category cate { get; set; }
        public List<Product> pro { get; set; }
    }
}