using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CVHomepage.Models
{
    public class CV
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public string User { get; set; }


        public virtual ICollection<Skill> Skills { get; set; }





    }
}