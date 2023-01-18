using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS_Toys.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["userName"] = GetCookie.Get(Request, "userName");
            return View();
        }

        public ActionResult About()
        {
            ViewData["userName"] = GetCookie.Get(Request, "userName");

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewData["userName"] = GetCookie.Get(Request, "userName");

            ViewBag.Message = "If you encounter any issues, we can be contacted through our e-mail adresses : mstoys@bussines.com";

            return View();
        }
    }
}