using Kdg_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kdg_MVC.Controllers
{
    [Authorize] 
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (RoleOfUser() == "Admin")
                {
                    ViewBag.displayMenu = "Admin";
                    return View();
                }
                else if (RoleOfUser() == "Staff")
                {
                    ViewBag.displayMenu = "Staff";
                    return View();
                }
                else if (RoleOfUser() == "Manager")
                {
                    ViewBag.displayMenu = "Manager";
                    return View();
                }
                else if (RoleOfUser() == "Parent")
                {
                    ViewBag.displayMenu = "Parent";
                    return View();
                }               
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();
        }

        public string RoleOfUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());

                return s[0].ToString();                
            }

            else
            {
                return "Not Auth";
            }
           
        }
    }
}