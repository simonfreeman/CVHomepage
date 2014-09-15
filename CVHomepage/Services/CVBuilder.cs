using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CVHomepage.Models;
using System.Text;

// This code sucks pretty hard, it's unflexible, it's repeating, and just generally bad. It is very much
// a placeholder until I code something smarter/better/non-nightmare-inducing.
// Unfortunately I need this site up and running yesterday, so it's either this or not be able to build my cv
// in my cv builder demo. A much better solution is hopefully on the way. Though for it to not be horrible
// hard code and still generate "nice" looking cvs, there has to be a rethink on how a user can specify
// what should be bullet points etc.
namespace CVHomepage.Services
{
    public class CVBuilder
    {
        public static string Build(CV cv)
        {
            StringBuilder secondaryEducationString= new StringBuilder();
            StringBuilder universityString = new StringBuilder();
            StringBuilder finalEducationString = new StringBuilder();
            StringBuilder softwareString = new StringBuilder();
            StringBuilder webString = new StringBuilder();
            StringBuilder computingString = new StringBuilder();
            StringBuilder workString = new StringBuilder();
            StringBuilder additionalString = new StringBuilder();
            bool secondaryEducation, university, software, web, computing, work, additional;
           secondaryEducation = software = web = computing = work = additional = university = false;


            foreach (var skill in cv.Skills)
            { 
                switch (skill.Category.Name)
                {
                    case "Education":
                        if (secondaryEducation == false && university == false)
                        {
                            finalEducationString.Append("<h2>");
                            finalEducationString.Append(@skill.Category.Name);
                            finalEducationString.Append("</h2>");
                        }

                        if( skill.Tags.Any(s => s.Name == "Secondary Education"))
                        {
                            if (secondaryEducation == false)
                            {
                                secondaryEducationString.Append("<h3>");
                                secondaryEducationString.Append("Secondary Education");
                                secondaryEducationString.Append("</h3>");
                                secondaryEducation = true;
                            }
                            secondaryEducationString.Append("<p class='list'> ");
                            secondaryEducationString.Append(@skill.CVText);
                            secondaryEducationString.Append("</p>");
                        }

                        if (skill.Tags.Any(s => s.Name == "University"))
                        {
                            if (university == false)
                            {
                                universityString.Append("<h3>");
                                universityString.Append("University");
                                universityString.Append("</h3>");
                                university = true;
                            }
                            universityString.Append("<p class='list'> ");
                            universityString.Append(@skill.CVText);
                            universityString.Append("</p>");
                        }
                        
                        
                        
                        break;

                    case "Software Development":
                        if (software == false)
                        {
                            softwareString.Append("<h2>");
                            softwareString.Append(@skill.Category.Name);
                            softwareString.Append("</h2>");
                            software = true;
                        }
                        softwareString.Append("<p class='list'> ");
                        softwareString.Append(@skill.CVText);
                        softwareString.Append("</p>");
                        break;

                    case "Web Development":
                        if (web == false)
                        {
                            webString.Append("<h2>");
                            webString.Append(@skill.Category.Name);
                            webString.Append("</h2>");
                            web = true;
                        }
                        webString.Append("<p class='list'> ");
                        webString.Append(@skill.CVText);
                        webString.Append("</p>");
                        break;

                    case "General Computing":
                        if (computing == false)
                        {
                            computingString.Append("<h2>");
                            computingString.Append(@skill.Category.Name);
                            computingString.Append("</h2>");
                            computing = true;
                        }
                        computingString.Append("<p class='list'> ");
                        computingString.Append(@skill.CVText);
                        computingString.Append("</p>");
                        break;

                    case "Work Experience":
                        if (work == false)
                        {
                            workString.Append("<h2>");
                            workString.Append(@skill.Category.Name);
                            workString.Append("</h2>");
                            work = true;
                        }
                        workString.Append("<h3>");
                        workString.Append(@skill.Name);
                        workString.Append("</h3>");
                        workString.Append("<p class='list'> ");
                        workString.Append(@skill.CVText);
                        workString.Append("</p>");
                        break;

                    case "Additional Skills and Interests":
                        if (additional == false)
                        {
                            additionalString.Append("<h2>");
                            additionalString.Append(@skill.Category.Name);
                            additionalString.Append("</h2>");
                            additional = true;
                        }
                        additionalString.Append("<h3>");
                        additionalString.Append(@skill.Name);
                        additionalString.Append("</h3>");
                        additionalString.Append("<p> ");
                        additionalString.Append(@skill.CVText);
                        additionalString.Append("</p>");
                        break;
                }
            }

            finalEducationString.Append(universityString.ToString());
            finalEducationString.Append(secondaryEducationString.ToString());
            StringBuilder finalString = new StringBuilder();
            finalString.Append(finalEducationString.ToString());
            finalString.Append(softwareString.ToString());
            finalString.Append(webString.ToString());
            finalString.Append(computingString.ToString());
            finalString.Append(workString.ToString());
            finalString.Append(additionalString.ToString());
            return finalString.ToString();
        }  
    
    }
}