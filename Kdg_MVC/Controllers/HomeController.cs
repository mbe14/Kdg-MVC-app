using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kdg_MVC.DataAccessLayer;
using Kdg_MVC.ViewModels;

namespace Kdg_MVC.Controllers
{
    public class HomeController : Controller
    {
        private AppContext db = new AppContext();
        public ActionResult Index()
        {        
                return View();                                
        }

        public ActionResult Statistics()
        {
            IQueryable<EnrollmentDateGroup> data = from child in db.Enrollments
                                                   group child by child.EnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       ChildrenCount = dateGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult EnrollmentData()
        {
            if (Request.IsAuthenticated && User.IsInRole("Parent"))
            {
                var data = from a in db.Enrollments
                           join c in db.Children on a.CID equals c.CID
                           join g in db.Groups on a.GroupID equals g.GroupID
                           where c.ContactEmail == User.Identity.Name
                           select new EnrollmentData
                           {
                               FullName = c.FirstName + " " + c.LastName,
                               Date = a.EnrollmentDate,
                               GroupName = g.GroupName
                           };

                ViewBag.FullName = (from item in data
                                    select item.FullName).FirstOrDefault();
                ViewBag.Date = (from item in data
                                select item.Date).FirstOrDefault().ToShortDateString();

                ViewBag.GroupName = (from item in data
                                     select item.GroupName).FirstOrDefault().ToString();

                return View(data.ToList());
            }

            else
            {
                return View();
            }
        }

        public ActionResult Payments()
        {
            var data = from p in db.Payments
                       join c in db.Children on p.CID equals c.CID
                       join f in db.FeeTypes on p.FeeID equals f.FeeID
                       where c.ContactEmail == User.Identity.Name
                       select new PaymentsList { 
                            FullName = c.FirstName +" " + c.LastName,
                            Amount = p.Amount,                     
                            Date = p.Date,
                            FeeType = f.FeeType
                       };
            string FullName = data.Select(d => d.FullName).FirstOrDefault();
            decimal amt = Convert.ToDecimal(data.Sum(d => d.Amount));
            ViewBag.FullName = FullName;
            ViewBag.Amount = amt;
            if (User.Identity.IsAuthenticated)
            {
                return View(data.ToList());
            }

            else
            {
                return new HttpUnauthorizedResult("Unauthorized");
            }          
        }

        public ActionResult Attendance()
        {            
            if (User.IsInRole("Admin"))
            {
               var data = from a in db.DailyAttendances
                           join c in db.Children on a.CID equals c.CID                           
                           select new AttendanceList
                           {
                               FullName = c.FirstName + " " + c.LastName,
                               Date = a.Att_Date,
                               isPresent = a.isPresent == true ? "Yes" : "No",
                               Notes = a.Notes
                           };
               if (User.Identity.IsAuthenticated)
               {
                   return View(data.ToList());
               }

               else
               {
                   return new HttpUnauthorizedResult("Unauthorized");
               }
            }

            else
            {
               var data = from a in db.DailyAttendances
                           join c in db.Children on a.CID equals c.CID
                           where c.ContactEmail == User.Identity.Name
                           select new AttendanceList
                           {
                               FullName = c.FirstName + " " + c.LastName,
                               Date = a.Att_Date,
                               isPresent = a.isPresent == true ? "Yes" : "No",
                               Notes = a.Notes
                           };
               int DaysYes = data.Count(a => a.isPresent == "Yes");
               int DaysNo = data.Count(a => a.isPresent == "No");
               string FullName = data.Select(d => d.FullName).FirstOrDefault();

               ViewBag.FullName = FullName;
               ViewBag.DaysYes = DaysYes;
               ViewBag.DaysNo = DaysNo;

               if (User.Identity.IsAuthenticated)
               {
                   return View(data.ToList());
               }

               else
               {
                   return new HttpUnauthorizedResult("Unauthorized");
               }
            }                                             
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}