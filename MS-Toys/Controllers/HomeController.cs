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

            ViewBag.Message = "Our store is the best. Period.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewData["userName"] = GetCookie.Get(Request, "userName");

            ViewBag.Message = "If you encounter any issues, contact us through our e-mail address: ms_toys_marketing@gmail.com";

            return View();
        }
    }
}
