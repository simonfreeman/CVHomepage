//credit to Chris Way for the base of this at http://chrisondotnet.com/2012/08/setting-active-link-twitter-bootstrap-navbar-aspnet-mvc/

using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace CVHomepage.Helpers.HTMLHelpers
{
    public static class HtmlHelpers
    {

        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            var builder = new TagBuilder("li")
            {
                InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString()
            };

            if (controllerName == currentController && actionName == currentAction)
                builder.AddCssClass("active");

            return new MvcHtmlString(builder.ToString()); 
        }

        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string[] additionalControllerNames)
        {

            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            List<string> additionalNameList = new List<string>(additionalControllerNames);


            var builder = new TagBuilder("li")
            {
                InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString()
            };

            if (controllerName == currentController || additionalNameList.Contains(currentController))
                
                builder.AddCssClass("active");

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string[] additionalControllerNames, string[,] ignoreActionControllerNames)
        {

            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            List<string> additionalNameList = new List<string>(additionalControllerNames);

            var builder = new TagBuilder("li")
            {
                InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString()
            };

            if (controllerName == currentController || additionalNameList.Contains(currentController))
            {
                //checks the current action and controller against the list of 
                //actions and controller a menu link should ignore. This is for instances 
                //like where planned features is in the cv controller, but you wouldnt want to light up 
                //cv manager on the sidebar.
                int count = 0;
                string checkIgnore = "";
                foreach (string ignore in ignoreActionControllerNames)
                {
                    if (count % 2 == 0)
                    {
                        checkIgnore = ignore;
                    }
                    else
                    {
                        checkIgnore += ignore;
                    }

                    if (checkIgnore == currentAction + currentController)
                    {
                        return new MvcHtmlString(builder.ToString());
                    }
                    count++;
                }
                    builder.AddCssClass("active");
            }
            return new MvcHtmlString(builder.ToString());
        }
    }
}