using System.Web;
using System.Web.Mvc;

namespace MS_Toys.Controllers
{
    public class GetCookie
    {
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
    }
}