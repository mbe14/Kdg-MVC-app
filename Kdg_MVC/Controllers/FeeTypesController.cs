using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kdg_MVC.DataAccessLayer;
using Kdg_MVC.Models;

namespace Kdg_MVC.Controllers
{
    public class FeeTypesController : Controller
    {
        private AppContext db = new AppContext();

        // GET: FeeTypes
        public ActionResult Index()
        {
            return View(db.FeeTypes.ToList());
        }

        // GET: FeeTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeTypes feeTypes = db.FeeTypes.Find(id);
            if (feeTypes == null)
            {
                return HttpNotFound();
            }
            return View(feeTypes);
        }

        // GET: FeeTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeeID,FeeType")] FeeTypes feeTypes)
        {
            if (ModelState.IsValid)
            {
                db.FeeTypes.Add(feeTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feeTypes);
        }

        // GET: FeeTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeTypes feeTypes = db.FeeTypes.Find(id);
            if (feeTypes == null)
            {
                return HttpNotFound();
            }
            return View(feeTypes);
        }

        // POST: FeeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeeID,FeeType")] FeeTypes feeTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feeTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feeTypes);
        }

        // GET: FeeTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeTypes feeTypes = db.FeeTypes.Find(id);
            if (feeTypes == null)
            {
                return HttpNotFound();
            }
            return View(feeTypes);
        }

        // POST: FeeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeeTypes feeTypes = db.FeeTypes.Find(id);
            db.FeeTypes.Remove(feeTypes);
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
