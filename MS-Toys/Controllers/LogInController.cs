using MS_Toys.Models;
using StoreAdministration;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace MS_Toys.Controllers
{
    public class LogInController : Controller
    {
        public LogInController()
        {
            Log.Initialize();
        }

        private StoreDataContext db = new StoreDataContext();

        public ActionResult Create()
        {
            ViewData["username"] = GetCookie.Get(Request, "username");
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "Username,EncryptedPassword")] User user)
        {
            var getUser = Data.GetUser(db, user.Username);

            if (getUser == null) 
            {
                Trace.WriteLine("User '" + user.Username + "' was not found");
                return View(user);
            }

            HttpCookie usernameCookie = new HttpCookie("username");
            usernameCookie["Username"] = user.Username;
            Response.Cookies.Add(usernameCookie);

            ViewData["username"] = user.Username;

            Trace.WriteLine("User '" + user.Username + "' has signed up");

            return Redirect("/Home/Index");
        } 
    }
}
