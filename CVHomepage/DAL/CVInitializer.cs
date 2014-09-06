using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CVHomepage.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace CVHomepage.DAL
{
    public class CVInitializer : System.Data.Entity.DropCreateDatabaseAlways<CVContext>
    {
        protected override void Seed(CVContext context)
        {
            //Skills
            var skills = new List<Skill>
            {
              new Skill { Name = "University Software Development", CVText = "Software development classes used a variety of languages, including C#. A grade of 82% was achieved in Computer Programming."},
              new Skill{Name = "University Business Law",CVText = "Law modules covered highly relevant areas such as the Computer Misuse Act and the Data Protection Act." }
            };

            skills.ForEach(s => context.Skills.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            //Categories
            var categories = new List<Category>
            {
                new Category{Name = "Education", Description = "Any formal study. Not too much detail."},
                new Category{Name = "Web Development", Description = "Skills directly linked to making websites."}
            };

            categories.ForEach(c => context.Categories.AddOrUpdate(b => b.Name, c));
            context.SaveChanges();

            //Tags
            var tags = new List<Tag>
            {
                new Tag{Name = "Web", Description = "Anything to do with websites."},
                new Tag{Name = "Computing", Description = "Anything to do with computer use."},
                new Tag {Name = "Universty", Description = "Skills studied at university."},
                new Tag {Name = "Business", Description = "Skills used in business."},
                new Tag {Name = "Computing", Description = "Anything related to computer use."}
            };

            tags.ForEach(t => context.Tags.AddOrUpdate(c => c.Name, t));
            context.SaveChanges();

            //SkillTags
            var skillTags = new List<SkillTag>
            {
                new SkillTag{SkillID=1, TagID =2},
                new SkillTag{SkillID=1, TagID =3},
                new SkillTag{SkillID=1, TagID =5},
                new SkillTag{SkillID=2, TagID =1},
                new SkillTag{SkillID=2, TagID =2},
                new SkillTag{SkillID=2, TagID =3},
                new SkillTag{SkillID=2, TagID =4},
                new SkillTag{SkillID=2, TagID =5},
            };
            skillTags.ForEach(s => context.SkillTags.Add(s));
            context.SaveChanges();
        }
    }
}