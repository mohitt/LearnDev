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
    
    public partial class Ad
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public int AdTypeId { get; set; }
        public Nullable<int> AddressId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual AdType AdType { get; set; }
        public virtual Address Address { get; set; }
    }
}
