using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CVHomepage.Models
{
    public class Education
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Name of School/University")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Qualifications Recieved/Expected, ex: \"10 GCSES A*-C\"")]
        public string Qualification { get; set; }


        [Display(Name = "Optional Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string User { get; set; }


        public virtual UserProfile UserProfile { get; set; }
    }
}