using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// credit to JM Alarcon for this at http://www.campusmvp.net/blog/how-to-manage-session-variables-like-a-pro
namespace CVHomepage.Helpers.SessionHelpers
{

    public class SessionHelpers
    {
        //Session variable constants
        public const string SKILLLIST = "CurrentSkills";


        public static T Read<T>(string variable)
        {
            object value = HttpContext.Current.Session[variable];
            if (value == null)
                return default(T);
            else
                return ((T)value);
        }

        public static void Write(string variable, object value)
        {
            HttpContext.Current.Session[variable] = value;
        }


        public static List<int> CurrentSkills
        {
            get
            {
                return Read<List<int>>(SKILLLIST);
            }
            set
            {
                Write(SKILLLIST, value);
            }
        }

  
    }
}
