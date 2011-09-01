using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ads.Model.Entities
{
   

    public partial class User : BaseEntity<int, int>
    {
        public User()
        {
            this.Ads = new HashSet<Ad>();
        }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Display Name Should be between 3 to 20 charcters")]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Real Name Should be between 3 to 40 charcters")]
        [DisplayName("Real Name")]
        public string RealName { get; set; }

        [Required]
        [RegularExpression(@"^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$", ErrorMessage = "Invalid Email")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Email Should be between 5 to 40 charcters")]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        public string OpenId { get; set; }
       
        public virtual ICollection<Ad> Ads { get; set; }
    }

    
}
