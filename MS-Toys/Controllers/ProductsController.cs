using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MS_Toys.Models;
using StoreAdministration;

namespace MS_Toys.Controllers
{
    public class ProductsController : Controller
    {
        private StoreDataContext db = new StoreDataContext();

        // GET: Products
        public ActionResult Index()
        {
            ViewData["username"] = GetCookie.Get(Request, "username");
            return View(db.Products.ToList());
        }

        // GET
        public ActionResult Purchase()
        {
            ViewData["username"] = GetCookie.Get(Request, "username");
            return View();
        }

        [HttpPost]
        public ActionResult Purchase([Bind(Include = "Id,Quantity")] Product product)
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            try
            {
                Data.SellProducts(db, ViewData["username"].ToString(), product.Id, 1);
            }
            catch (ProductException)  // TODO log here
            {
            }
            catch (QuantityException)
            {
            }
            
            return RedirectToAction("Index");
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewData["username"] = GetCookie.Get(Request, "username");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Quantity")] Product product)
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            if (ModelState.IsValid)
            {
                Data.InsertProduct(db, product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(long? id)
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,Quantity")] Product product)
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(long? id)
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
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
