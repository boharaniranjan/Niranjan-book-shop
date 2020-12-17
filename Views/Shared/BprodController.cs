using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Niranjan_Book_Shop;

namespace Niranjan_Book_Shop.Views.Shared
{
    public class BprodController : Controller
    {
        private Niranjan_Book_shopEntities2 db = new Niranjan_Book_shopEntities2();

        // GET: Bprod
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.Order);
            return View(products.ToList());
        }

        // GET: Bprod/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Bprod/Create
        public ActionResult Create()
        {
            ViewBag.o_id = new SelectList(db.Orders, "order_id", "order_name");
            return View();
        }

        // POST: Bprod/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,p_name,p_price,p_qty,p_details,cat_id,o_id")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.o_id = new SelectList(db.Orders, "order_id", "order_name", product.o_id);
            return View(product);
        }

        // GET: Bprod/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.o_id = new SelectList(db.Orders, "order_id", "order_name", product.o_id);
            return View(product);
        }

        // POST: Bprod/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,p_name,p_price,p_qty,p_details,cat_id,o_id")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.o_id = new SelectList(db.Orders, "order_id", "order_name", product.o_id);
            return View(product);
        }

        // GET: Bprod/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Bprod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
