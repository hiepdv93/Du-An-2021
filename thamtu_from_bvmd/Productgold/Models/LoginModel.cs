using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Productgold.Models
{
    public class LoginModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string SecurityKey { get; set; }
        public bool subAdmin { get; set; }
    }
}