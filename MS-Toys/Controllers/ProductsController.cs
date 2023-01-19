using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MS_Toys.Models;
using StoreAdministration;

namespace MS_Toys.Controllers
{
    public class ProductsController : Controller
    {
        public ProductsController()
        {
            Log.Initialize();
        }

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
            return View(new Product());
        }

        [HttpPost]
        public ActionResult Purchase([Bind(Include = "Id,Quantity")] Product product)
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            try
            {
                Data.SellProducts(db, ViewData["username"].ToString(), product.Id, 1);
                Trace.WriteLine("User '" + ViewData["username"].ToString() + "' has purchased "
                    + product.Quantity + " units of Product '" + product.Id + "'");
            }
            catch (ProductException)  // TODO log here
            {
                Trace.WriteLine("Product '" + product.Id + "' was not found");
            }
            catch (QuantityException)
            {
                Trace.WriteLine("Product '" + product.Id + "' quantity too low");
            }

            return RedirectToAction("Index");
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewData["username"] = GetCookie.Get(Request, "username");
            return View(new Product());
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
                Trace.WriteLine("Product '" + product.Id + "' was added");

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
                Trace.WriteLine("Product '" + product.Id + "' was modified");

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
            Trace.WriteLine("Product '" + product.Id + "' was deleted");

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
