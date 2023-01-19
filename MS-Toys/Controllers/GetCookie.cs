using System.Web;
using System.Web.Mvc;
using MS_Toys.Models;

namespace MS_Toys.Controllers
{
    public class GetCookie
    {
        //Method requests a cookie and returns a string
        //Cookie will be used to retrieve the username of the logged in user
        public static string Get(HttpRequestBase request, string cookieName)
        {
            string username = string.Empty;
            HttpCookie reqCookies = request.Cookies[cookieName];
         
            if (reqCookies != null)
            {
                username = reqCookies["UserName"].ToString();
            }
            return username;
        }

        //Cookie receives the logged in username and is stored until browser exit
        public static void UsernameCookie(HttpResponseBase response,User user)
        {
            HttpCookie usernameCookie = new HttpCookie("username");
            usernameCookie["username"] = user.Username;
            response.Cookies.Add(usernameCookie);
        }
    }
}