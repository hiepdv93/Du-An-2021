//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NTSPRODUCT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.News = new HashSet<News>();
            this.Products = new HashSet<Product>();
        }
    
        public string id { get; set; }
        public string cateName { get; set; }
        public string cateKey { get; set; }
        public string cateLang { get; set; }
        public string titleSeo { get; set; }
        public string keySeo { get; set; }
        public string desSeo { get; set; }
        public string cateDescription { get; set; }
        public string cateImage { get; set; }
        public string cssClass { get; set; }
        public string cateicon { get; set; }
        public string catepar_id { get; set; }
        public Nullable<int> cateOrder { get; set; }
        public Nullable<int> cate_cap { get; set; }
        public Nullable<int> cateType { get; set; }
        public Nullable<bool> cateActiveHome { get; set; }
        public Nullable<bool> cateActive { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public string cateLevel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
