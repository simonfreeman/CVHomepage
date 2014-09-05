using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CVHomepage.Models
{
    public class Skill
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name{ get; set; }

        
        public string Notes { get; set; }

        [Required]
        public string CVText { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        
        public virtual Category Category { get; set; }


      
    }
}