using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTSPRODUCT.Models
{
    public static class ConfigModel
    {
        public static List<Config> listConfig;
        public static List<Category> listCate;
    }
    public class ProductHome
    {
        public Category Item { get; set; }
        public List<Category> ListItem { get; set; }
        public List<Product> ListProduct { get; set; }
    }
}