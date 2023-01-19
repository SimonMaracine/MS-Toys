using MS_Toys.Models;
using System.Web;

namespace MS_Toys.Controllers
{
    public class Cookie
    {
        //Method requests a cookie and returns a string
        //Cookie will be used to retrieve the username of the logged in user
        public static string Get(HttpRequestBase request, string cookieName)
        {
            string username = string.Empty;
            HttpCookie cookie = request.Cookies[cookieName];
         
            if (cookie != null)
            {
                username = cookie["username"].ToString();
            }

            return username;
        }

        //Cookie receives the logged in username and is stored until browser exit
        public static void UsernameCookie(HttpResponseBase response, User user)
        {
            HttpCookie usernameCookie = new HttpCookie("username");
            usernameCookie["username"] = user.Username;
            response.Cookies.Add(usernameCookie);
        }

        /*public static void Clear(HttpResponseBase response, HttpRequestBase request, string cookieName)
        {
            HttpCookie cookie = request.Cookies[cookieName];

            if (cookie != null)
            {
                cookie["username"] = string.Empty;

                response.Cookies.Add(cookie);
                request.Cookies.Add(cookie);

                Trace.WriteLine($"Cleared cookie `{cookieName}`");
            }
        }

        public static void Delete(HttpResponseBase response, HttpRequestBase request, string cookieName)
        {
            HttpCookie cookie = request.Cookies[cookieName];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);

                response.Cookies.Add(cookie);

                Trace.WriteLine($"Deleted cookie `{cookieName}`");
            }
        }*/
    }
}
