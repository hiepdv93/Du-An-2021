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
    
    public partial class ProductExten
    {
        public int id { get; set; }
        public Nullable<int> cateid { get; set; }
        public Nullable<int> brandid { get; set; }
        public string proCode { get; set; }
        public string pro_name { get; set; }
        public string pro_key { get; set; }
        public Nullable<double> proPrice { get; set; }
        public Nullable<double> proPrice_sale { get; set; }
        public string proAvata { get; set; }
        public string proAvataId { get; set; }
        public string proImages { get; set; }
        public string proImagesId { get; set; }
        public string proWant { get; set; }
        public string proContents4 { get; set; }
        public string proContents3 { get; set; }
        public string proContents2 { get; set; }
        public string proContents { get; set; }
        public string proFile { get; set; }
        public Nullable<int> pro_count { get; set; }
        public Nullable<int> pro_view { get; set; }
        public Nullable<bool> pro_hot { get; set; }
        public Nullable<bool> pro_new { get; set; }
        public Nullable<bool> pro_sale { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<bool> statust { get; set; }
        public string ex1 { get; set; }
        public string prodescription { get; set; }
        public Nullable<int> proOrder { get; set; }
        public string titleSeo { get; set; }
        public string keySeo { get; set; }
        public string desSeo { get; set; }
        public string pLang { get; set; }
        public string pTag { get; set; }
        public string siteTPA { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}
