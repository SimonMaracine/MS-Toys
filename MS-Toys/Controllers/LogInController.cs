using MS_Toys.Models;
using StoreAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MS_Toys.Controllers
{
    public class LogInController : Controller
    {
        private StoreDataContext db = new StoreDataContext();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "Username, EncryptedPassword")] User user)
        {
            var getUser = Data.GetUser(db, user.Username);

            if(getUser == null) 
            {
                return View(user);
            }

            HttpCookie userNameCookie = new HttpCookie("userName");
            userNameCookie["Username"] = user.Username;
            Response.Cookies.Add(userNameCookie);

            return View(user);
        } 
    }
}