//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ads.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address
    {
        public Address()
        {
            this.Ads = new HashSet<Ad>();
        }
    
        public int Id { get; set; }
        public string Property { get; set; }
    
        public virtual City City { get; set; }
        public virtual ICollection<Ad> Ads { get; set; }
    }
}
