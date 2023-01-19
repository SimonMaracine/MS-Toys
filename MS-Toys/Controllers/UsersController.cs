using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MS_Toys.Models;
using StoreAdministration;

namespace MS_Toys.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {
            Log.Initialize();
        }

        private StoreDataContext db = new StoreDataContext();

        // GET: Users
        public ActionResult Index()
        {
            ViewData["username"] = Cookie.Get(Request, "username");
            return View(db.Users.ToList());
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewData["username"] = Cookie.Get(Request, "username");
            return View(new User());
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,FirstName,LastName,EncryptedPassword")] User user)
        {
            ViewData["username"] = Cookie.Get(Request, "username");

            if (ModelState.IsValid)
            {
                var users = Data.GetAllUsernames(db);

                foreach(var user_ in users)
                {
                    if(user_.Username == user.Username)
                    {
                        return View(user);
                    }
                }

                Data.SignUserUp(db, user.Username, user.EncryptedPassword, user.FirstName, user.LastName);
                Trace.WriteLine("User '" + user.Username + "' has signed up");

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            ViewData["username"] = Cookie.Get(Request, "username");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,FirstName,LastName,EncryptedPassword")] User user)
        {
            ViewData["username"] = Cookie.Get(Request, "username");

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Trace.WriteLine("User '" + user.Username + "' was edited");

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            ViewData["username"] = Cookie.Get(Request, "username");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ViewData["username"] = Cookie.Get(Request, "username");

            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            Trace.WriteLine("User '" + user.Username + "' was deleted");

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
