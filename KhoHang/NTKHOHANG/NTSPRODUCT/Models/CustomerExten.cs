using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTSPRODUCT.Models;
namespace NTSPRODUCT.Models
{
    public class CustomerExten : Customer
    {
        public int Level { get; set; }
        public string CodeLogin { get; set; }
        public string CodeIntro { get; set; }
    }

    public class UserInfo
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}