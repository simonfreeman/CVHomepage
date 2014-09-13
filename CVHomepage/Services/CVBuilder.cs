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
            StringBuilder educationString= new StringBuilder();
            StringBuilder softwareString = new StringBuilder();
            StringBuilder webString = new StringBuilder();
            StringBuilder computingString = new StringBuilder();
            StringBuilder workString = new StringBuilder();
            StringBuilder additionalString = new StringBuilder();
            bool education, software, web, computing, work, additional;
           education = software = web = computing = work = additional = false;


            foreach (var skill in cv.Skills)
            { 
                switch (skill.Category.Name)
                {
                    case "Education":
                        if (education == false)
                        {
                            educationString.Append("<h2>");
                            educationString.Append(@skill.Category.Name);
                            educationString.Append("</h2>");
                            education = true;
                        }
                        
                        educationString.Append("<p class='list'> ");
                        educationString.Append(@skill.CVText);
                        educationString.Append("</p>");
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
            StringBuilder finalString = new StringBuilder();
            finalString.Append(educationString.ToString());
            finalString.Append(softwareString.ToString());
            finalString.Append(webString.ToString());
            finalString.Append(computingString.ToString());
            finalString.Append(workString.ToString());
            finalString.Append(additionalString.ToString());
            return finalString.ToString();
        }  
    
    }
}