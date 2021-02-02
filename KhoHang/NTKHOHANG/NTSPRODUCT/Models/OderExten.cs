using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTSPRODUCT.Models;
namespace NTSPRODUCT.Models
{
    public class OderExten : Oder
    {
        public string fullName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string addresss { get; set; }
        public int? amount { get; set; }
        public int levelAcount { get; set; }
        public string parentId { get; set; }
        public string parentName { get; set; }

    }
}