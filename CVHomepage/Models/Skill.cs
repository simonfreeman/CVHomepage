using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CVHomepage.Models
{
    public class Skill
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name{ get; set; }

        [DisplayFormat(NullDisplayText = "No notes.")]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "CV Text")]
        [DataType(DataType.MultilineText)]
        public string CVText { get; set; }

        [Required]
        public string User { get; set; }
        
        public int CategoryID { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        
        public virtual Category Category { get; set; }

        public virtual ICollection<CV> CVs { get; set; }

      
    }
}