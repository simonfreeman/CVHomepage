using System.ComponentModel.DataAnnotations;

namespace CVHomepage.ViewModels
{
    public class Contact
    {
        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Your Message")]
        [DataType(DataType.MultilineText)]
        public string MessageBody { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Your Email")]
        public string Email { get; set; }
    }
}