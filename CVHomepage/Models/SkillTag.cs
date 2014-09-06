using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVHomepage.Models
{
    public class SkillTag
    {
        public int ID { get; set; }
        public int SkillID { get; set; }
        public int TagID { get; set; }

        public virtual Skill Skill { get; set; }
        public virtual Tag Tag { get; set; }
    }
}