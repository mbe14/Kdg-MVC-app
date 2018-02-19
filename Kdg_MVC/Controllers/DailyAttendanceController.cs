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
    [Authorize]
    public class DailyAttendanceController : Controller
    {
        private AppContext db = new AppContext();

        // GET: DailyAttendance
        public ActionResult Index()
        {                       
            var da = db.DailyAttendances.Include(c => c.Children);
            return View(da.ToList());
                                    
        }

        // GET: DailyAttendance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyAttendance dailyAttendance = db.DailyAttendances.Find(id);
            if (dailyAttendance == null)
            {
                return HttpNotFound();
            }
            return View(dailyAttendance);
        }

        // GET: DailyAttendance/Create
        public ActionResult Create()
        {
            ViewBag.CID = new SelectList((from c in db.Children
                                          select new
                                          {
                                              CID = c.CID,
                                              FullName = c.FirstName + " " + c.LastName
                                          }),
                                              "CID",
                                              "FullName",
                                              null);
            return View();
        }

        // POST: DailyAttendance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendanceID,Att_Date,CID,isPresent,Notes")] DailyAttendance dailyAttendance)
        {
            if (ModelState.IsValid)
            {
                db.DailyAttendances.Add(dailyAttendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }            
            ViewBag.CID = new SelectList(db.Children, "CID", "FullName", dailyAttendance.CID);
            return View(dailyAttendance);
        }

        // GET: DailyAttendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyAttendance dailyAttendance = db.DailyAttendances.Find(id);
            if (dailyAttendance == null)
            {
                return HttpNotFound();
            }           
            ViewBag.CID = new SelectList(db.Children, "CID", "FullName", dailyAttendance.CID);
            return View(dailyAttendance);
        }

        // POST: DailyAttendance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendanceID,Att_Date,CID,isPresent,Notes")] DailyAttendance dailyAttendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailyAttendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dailyAttendance);
        }

        // GET: DailyAttendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyAttendance dailyAttendance = db.DailyAttendances.Find(id);
            if (dailyAttendance == null)
            {
                return HttpNotFound();
            }
            return View(dailyAttendance);
        }

        // POST: DailyAttendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyAttendance dailyAttendance = db.DailyAttendances.Find(id);
            db.DailyAttendances.Remove(dailyAttendance);
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
