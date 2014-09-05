using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CVHomepage.Models
{
    public class Skill
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name;

        public string Notes;

        [Display(Name = "CV Text")]
        public string CVText;

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual Category Category { get; set; }
    }
}