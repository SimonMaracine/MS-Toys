using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace MS_Toys.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            Log.Initialize();
        }

        public ActionResult Index()
        {
            ViewData["username"] = GetCookie.Get(Request, "username");
            return View();
        }

        public ActionResult About()
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            ViewBag.Message = "Our store is the best. Period.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            ViewBag.Message = "If you encounter any issues, contact us through our e-mail address: ms_toys_marketing@gmail.com";

            return View();
        }
    }
}
