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
    
    public partial class Oder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oder()
        {
            this.Oderdts = new HashSet<Oderdt>();
        }
    
        public int id { get; set; }
        public Nullable<int> cusid { get; set; }
        public Nullable<double> total { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public Nullable<int> statust { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string Address { get; set; }
        public string oLang { get; set; }
        public string siteTPA { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oderdt> Oderdts { get; set; }
    }
}
