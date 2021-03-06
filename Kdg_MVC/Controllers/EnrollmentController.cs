﻿using System;
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
    public class EnrollmentController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Enrollment
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Groups);
            enrollments = db.Enrollments.Include(i => i.Instructors);
            enrollments = db.Enrollments.Include(c => c.Children);
            return View(enrollments.ToList());

        }

        // GET: Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName");
            ViewBag.InstructorID = new SelectList((from i in db.Instructors
                                                   select new
                                                   {
                                                       InstructorID = i.InstructorID,
                                                       FullName = i.FirstName + " " + i.LastName
                                                   }),
                "InstructorID",
                "FullName",
                null);
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

        // POST: Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,InstructorID,GroupID,CID,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", enrollment.GroupID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "InstructorID", "FullName", enrollment.InstructorID);
            ViewBag.CID = new SelectList(db.Children, "CID", "FullName", enrollment.CID);

            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", enrollment.GroupID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "InstructorID", "FullName", enrollment.InstructorID);
            ViewBag.CID = new SelectList(db.Children, "CID", "FullName", enrollment.CID);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,InstructorID,GroupID,CID,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", enrollment.GroupID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "InstructorID", "FullName", enrollment.InstructorID);
            ViewBag.CID = new SelectList(db.Children, "CID", "FullName", enrollment.CID);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
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
