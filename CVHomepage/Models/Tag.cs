using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CVHomepage.Models
{
    public class Tag 
    {

        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Description { get; set; }
        public virtual ICollection<SkillTag> SkillTags { get; set; }
        
    }
}