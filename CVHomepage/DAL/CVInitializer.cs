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


            

            //Categories.
            Category cat1 = new Category{ Name = "Education", Description = "Any formal study. Not too much detail.",
                Skills = new List<Skill>()};
            Category cat2 = new Category{ Name = "Web Development", Description = "Skills directly linked to making websites.",
                Skills = new List<Skill>()};

            //Skills.
            Skill skill1 = new Skill { Name = "University Software Development", 
                CVText = "Software development classes used a variety of languages, including C#. A grade of 82% was achieved in Computer Programming.",
                Tags = new List<Tag>()};
            Skill skill2 = new Skill{Name = "University Business Law",
                CVText = "Law modules covered highly relevant areas such as the Computer Misuse Act and the Data Protection Act.",
                Tags = new List<Tag>()};
                
            //Tags.
            Tag tag1 = new Tag{Name = "Web", Description = "Anything to do with websites."};
            Tag tag2 = new Tag { Name = "Computing", Description = "Anything to do with computer use." };
            Tag tag3 = new Tag { Name = "University", Description = "Skills studied at university." };
            Tag tag4 = new Tag { Name = "Business", Description = "Skills used in business." };
            Tag tag5 = new Tag { Name = "Programming", Description = "Things related to creating code." };


            //Skills add tags before they get added to categories.
            skill1.Tags.Add(tag1);
            skill1.Tags.Add(tag3);
            skill1.Tags.Add(tag5);

          
            skill2.Tags.Add(tag1);
            skill2.Tags.Add(tag2);
            skill2.Tags.Add(tag3);
            skill2.Tags.Add(tag4);
            skill2.Tags.Add(tag5);

            //Add skills to categories.
            cat1.Skills.Add(skill1);
            cat1.Skills.Add(skill2);

            //Save categories to the database which will in turn add the skills and tags because they
            //are in the category objects that get saved, see above.
            context.Categories.Add(cat1);
            context.Categories.Add(cat2);

          //CVs
            CV cv1 = new CV { Name = "Default", Skills = new List<Skill>(), User = "blah"};
            CV cv2 = new CV { Name = "Test", Skills = new List<Skill>(), User = "blah"};

            cv1.Skills.Add(skill1);
            cv1.Skills.Add(skill2);
            cv2.Skills.Add(skill2);

            context.CVs.Add(cv1);
            context.CVs.Add(cv2);
           
        }
    }
}