//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace landSelling.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public post()
        {
            this.requests = new HashSet<request>();
        }
    
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string image { get; set; }
        public int price { get; set; }
        public string propertyType { get; set; }
        public string status { get; set; }
        public string mark { get; set; }
        public System.DateTime date { get; set; }
        public int area { get; set; }
        public int beds { get; set; }
        public int baths { get; set; }
        public int garage { get; set; }
        public int uid { get; set; }
    
        public virtual user user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<request> requests { get; set; }
    }
}
