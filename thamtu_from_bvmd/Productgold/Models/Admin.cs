//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Productgold.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Admin
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string addresss { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string depart { get; set; }
        public Nullable<bool> active { get; set; }
        public string siteTPA { get; set; }
        public string siteModul { get; set; }
    }
}