using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVHomepage.Models
{
    public class Category : Filter
    {

        public virtual ICollection<Skill> Skills { get; set; }
    }
}