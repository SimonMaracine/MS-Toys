using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MS_Toys.Models;
using StoreAdministration;

namespace MS_Toys.Controllers
{
    public class TransactionsController : Controller
    {
        public TransactionsController()
        {
            Log.Initialize();
        }

        private StoreDataContext db = new StoreDataContext();

        // GET: Transactions
        public ActionResult Index()
        {
            ViewData["username"] = GetCookie.Get(Request, "username");
            return View(db.Transactions.ToList());
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(long? id)
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Transaction transaction = db.Transactions.Find(id);

            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ViewData["username"] = GetCookie.Get(Request, "username");

            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            Trace.WriteLine("Transaction '" + transaction.Id + "' was deleted");

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
