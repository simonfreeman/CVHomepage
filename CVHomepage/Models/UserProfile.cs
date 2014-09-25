using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVHomepage.Models
{
    public class UserProfile
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Key]
        public string User { get; set; }


        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [StringLength(15, MinimumLength = 11)]
        public string Mobile{ get; set; }

        [StringLength(15, MinimumLength = 11)]
        public string Landline { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "House Name/Number")]
        public string House { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Town/City")]
        public string City { get; set; }

        [StringLength(10, MinimumLength = 1)]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Your Email")]
        public string Email { get; set; }



        public virtual ICollection<Education> Educations { get; set; }



    }
}