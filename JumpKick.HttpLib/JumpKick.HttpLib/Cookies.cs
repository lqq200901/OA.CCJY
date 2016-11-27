using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JumpKick.HttpLib
{
    public class Cookies
    {
        protected static CookieContainer cookies = new CookieContainer();

        public static void ClearCookies()
        {
            cookies = new CookieContainer();
        }

        public static void AddCookie(string name, string key, string domain)
        {
            try
            {
                cookies.Add(new Cookie(name, key, "", domain));
            }
            catch (Exception e)
            {
                string a = e.Message;
            }
        }

        public static CookieContainer Container { get { return cookies; } }
    }
}
