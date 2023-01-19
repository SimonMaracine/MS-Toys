﻿using MS_Toys.Models;
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
            ViewData["username"] = Cookie.Get(Request, "username");
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
                ViewData["username"] = Cookie.Get(Request, "username");

                return View(user);
            }

            HttpCookie usernameCookie = new HttpCookie("username");
            usernameCookie["username"] = user.Username;
            Response.Cookies.Add(usernameCookie);

            ViewData["username"] = user.Username;

            Trace.WriteLine("User '" + user.Username + "' has signed up");

            return Redirect("/Home/Index");
        }

        public ActionResult LogOut()
        {
            /*if (ViewData["username"] == null || ViewData["username"].ToString().Length == 0)
            {
                return View(new User());
            }*/

            // Cookie.Delete(Request, "username");
            // Request.Cookies.Clear();
            // Cookie.Clear(Response, Request, "username");

            // ViewData["username"] = Cookie.Get(Request, "username");

            return Redirect("/Home/Index");
        }
    }
}
