using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Model.Entities
{
    public partial class Ad : BaseEntity<int, int>
    {
        public string Description { get; set; }

        [Required]
        [DisplayName("Choose a Sub Category")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Title Should be between 3 to 20 charcters")]
        [DisplayName("Provide a Title")]
        public string Title { get; set; }
        public int UserId { get; set; }
        public int AdTypeId { get; set; }
        public Nullable<int> AddressId { get; set; }


        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual AdType AdType { get; set; }
        public virtual Address Address { get; set; }
    }
}
