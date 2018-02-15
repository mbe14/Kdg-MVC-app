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