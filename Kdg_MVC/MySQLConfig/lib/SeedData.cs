using Kdg_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kdg_MVC.MySQLConfig.lib
{
    public class SeedData
    {
        public const string _CreateTbChildren = @"CREATE TABLE `Children` (`CID` int(4) NOT NULL AUTO_INCREMENT, `FirstName` varchar(50) NOT NULL, `LastName` varchar(50) NOT NULL, `CNP` varchar(13) NOT NULL UNIQUE, `Address` varchar(50) NOT NULL, `City` varchar(50) NOT NULL, `MothersName` varchar(100) NOT NULL, `FathersName` varchar(100) NOT NULL, `ContactEMail` varchar(50) NOT NULL, PRIMARY KEY(`CID`));";

        public const string _CreateTbEnrollment = @"CREATE TABLE `Enrollment` (`EnrollmentID` int(4) NOT NULL AUTO_INCREMENT, `GroupID` int(4) NOT NULL, `InstructorID` int(4) NOT NULL, `CID` int(4) NOT NULL, `EnrollmentDate` DATE NOT NULL, PRIMARY KEY (`EnrollmentID`));";

        public const string _CreateTbGroup = @"CREATE TABLE `Group` (`GroupID` int(4) NOT NULL AUTO_INCREMENT, `GroupName` varchar(50) NOT NULL, PRIMARY KEY(`GroupID`));";

        public const string _CreateTbInstructor = @"CREATE TABLE `Instructor` (`InstructorID` int(4) NOT NULL AUTO_INCREMENT, `FirstName` varchar(50) NOT NULL, `LastName` varchar(50) NOT NULL, `StartDate` DATE NOT NULL, `EndDate` DATE, `PayRate` DECIMAL(10,2) NOT NULL, `EMail` varchar(100) NOT NULL, PRIMARY KEY (`InstructorID`));";

        public const string _CreateTbDailyAttendance = @"CREATE TABLE `DailyAttendance` (`AttendanceID` int(4) NOT NULL AUTO_INCREMENT, `Att_Date` DATE NOT NULL, `CID` int(4) NOT NULL, `isPresent` bool NOT NULL, `Notes` varchar(200), PRIMARY KEY (`AttendanceID`));";

        public const string _CreateTbPayments = @"CREATE TABLE `Payments` (`PaymentID` int(4) NOT NULL AUTO_INCREMENT, `CID` int(4) NOT NULL, `Amount` DECIMAL(10,2) NOT NULL, `Date` DATE NOT NULL, `FeeID` int(4) NOT NULL, PRIMARY KEY (`PaymentID`));";

        public const string _CreateTbFeeTypes = @"CREATE TABLE `FeeTypes` (`FeeID` int(4) NOT NULL AUTO_INCREMENT, `FeeType` varchar(50) NOT NULL, PRIMARY KEY (`FeeID`));";

        public const string _Add_Enrollment_fk0 = @"ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk0` FOREIGN KEY (`GroupID`) REFERENCES `Group`(`GroupID`);";

        public const string _Add_Enrollment_fk1 = @"ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk1` FOREIGN KEY (`InstructorID`) REFERENCES `Instructor`(`InstructorID`);";

        public const string _Add_Enrollment_fk2 = @"ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk2` FOREIGN KEY (`CID`) REFERENCES `Children`(`CID`);";

        public const string _Add_DailyAttendance_fk0 = @"ALTER TABLE `DailyAttendance` ADD CONSTRAINT `DailyAttendance_fk0` FOREIGN KEY (`CID`) REFERENCES `Children`(`CID`);";

        public const string _Add_Payments_fk0 = @"ALTER TABLE `Payments` ADD CONSTRAINT `Payments_fk0` FOREIGN KEY (`CID`) REFERENCES `Children`(`CID`);";

        public const string _Add_Payments_fk1 = @"ALTER TABLE `Payments` ADD CONSTRAINT `Payments_fk1` FOREIGN KEY (`FeeID`) REFERENCES `FeeTypes`(`FeeID`);";

        public static void Seed()
        {
            Kdg_MVC.DataAccessLayer.AppContext ctx = new DataAccessLayer.AppContext();
            var chldren = new List<Children>
            {
                new Children
                {
                    FirstName = "Jane", LastName = "Austen", CNP = "2021001125899", Address = "591 Myers Lane", City = "Roseville", MothersName = "Amanda", FathersName = "Mario", ContactEmail = "austen@test.com"
                },
                new Children
                {
                    FirstName = "Mike", LastName = "Jensen", CNP = "1150727108484", Address = "730 Pawnee Street", City = "Englewood", MothersName = "Lucy", FathersName = "Elvis", ContactEmail = "nacho@icloud.com"
                },
                new Children
                {
                    FirstName = "Jonathan", LastName = "Davis", CNP = "1160107158902", Address = "8436 West Greenview Ave.", City = "Jackson Heights", MothersName = "Haylie", FathersName = "Devin", ContactEmail = "graham@live.com"
                },
                new Children
                {
                    FirstName = "Harry", LastName = "Brown", CNP = "1150715167679", Address = "8436 West Greenview Ave.", City = "Jackson Heights", MothersName = "Janiya", FathersName = "Brooks", ContactEmail = "mddallara@mac.com"
                },
                new Children
                {
                    FirstName = "Juan", LastName = "Butler", CNP = "1150718140827", Address = "8436 West Greenview Ave.", City = "Jackson Heights", MothersName = "Elisa", FathersName = "Drew", ContactEmail = "wikinerd@optonline.net"
                },
                new Children
                {
                    FirstName = "Carolyn", LastName = "Mitchell", CNP = "2150716177951", Address = "692 Yukon Ave.", City = "Brunswick", MothersName = "Makenzie", FathersName = "Frank", ContactEmail = "durist@optonline.net"
                },
                new Children
                {
                    FirstName = "Thomas", LastName = "Perez", CNP = "1150510178127", Address = "9259 Coffee Circle", City = "Brunswick", MothersName = "Tia", FathersName = "Dario", ContactEmail = "aracne@yahoo.com"
                },
                new Children
                {
                    FirstName = "Pamela", LastName = "Lopez", CNP = "215093056670", Address = "579 Ocean Rd.", City = "Brunswick", MothersName = "Kaya", FathersName = "Easton", ContactEmail = "sabren@yahoo.com"
                },
                new Children
                {
                    FirstName = "Laura", LastName = "Carter", CNP = "2160923185545", Address = "7568 Fulton Lane", City = "Brunswick", MothersName = "Brynn", FathersName = "Dexter", ContactEmail = "arnold@icloud.com"
                },
                new Children
                {
                    FirstName = "Ralph", LastName = "Rodriguez", CNP = "115062152942", Address = "427 Hill Street", City = "Deer Park", MothersName = "Erin", FathersName = "Bradley", ContactEmail = "reeds@msn.com"
                },
                new Children
                {
                    FirstName = "Sean", LastName = "Garcia", CNP = "115022514636", Address = "834 Bay Street", City = "Deer Park", MothersName = "Natalya", FathersName = "Jayden", ContactEmail = "hamilton@optonline.net"
                },
                new Children
                {
                    FirstName = "Dorian", LastName = "Robinson", CNP = "1150916118807", Address = "253 4th St.", City = "Deer Park", MothersName = "Sienna", FathersName = "Jack", ContactEmail = "scottzed@comcast.net"
                },
                new Children
                {
                    FirstName = "Eugene", LastName = "Clark", CNP = "1150422115603", Address = "707 East Berkshire St.", City = "Deer Park", MothersName = "Josephine", FathersName = "Dax", ContactEmail = "yenya@verizon.net"
                },
                new Children
                {
                    FirstName = "Anna  ", LastName = "Parker", CNP = "216052157989", Address = "162 N. Fulton Dr.", City = "Deer Park", MothersName = "Lucy", FathersName = "Trey", ContactEmail = "ranasta@sbcglobal.net"
                },
                new Children
                {
                    FirstName = "Ralph", LastName = "Moore", CNP = "1150310171284", Address = "54 Essex Ave.", City = "Deer Park", MothersName = "Carly", FathersName = "Gunther", ContactEmail = "ninenine@live.com"
                }
            };

            chldren.ForEach(s => ctx.Children.Add(s));

            var instrcts = new List<Instructor>
            {
                new Instructor
                {
                    FirstName = "Mike", LastName="Johnson", PayRate=1200.2M, StartDate=Convert.ToDateTime("12/11/2012"), EMail="staff@test.com"
                },
                new Instructor
                {
                    FirstName = "Sam", LastName="Smith", PayRate=1200.2M, StartDate=Convert.ToDateTime("12/11/2012"), EMail="staff1@test.com"
                },
                 new Instructor
                {
                    FirstName = "Lucy", LastName="Liu", PayRate=1200.2M, StartDate=Convert.ToDateTime("12/11/2012"), EMail="staff2@test.com"
                }
            };

            instrcts.ForEach(s => ctx.Instructors.Add(s));

            var grps = new List<Group>
            {
                new Group
                {
                    GroupName = "Grupa Mica"
                },
                new Group
                {
                    GroupName = "Grupa Mare"
                },
                new Group
                {
                    GroupName = "Licuricii"
                },
                new Group
                {
                    GroupName = "Grupa Mijlocie"
                }
               
            };

            grps.ForEach(s => ctx.Groups.Add(s));

            ctx.SaveChanges();
        }

        public static void AddUserRoles(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // creating Creating Staff role    
            if (!roleManager.RoleExists("Staff"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Staff";
                roleManager.Create(role);
            }
            // creating Creating Parent role    
            if (!roleManager.RoleExists("Parent"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Parent";
                roleManager.Create(role);

            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

            }
        }

        public static void SeedUsersToRoles(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string userPWD = "!Pass123";

            //Here we create a Admin super user who will maintain the website                  

            var admin = new ApplicationUser();
            admin.UserName = "mbenegui@gmail.com";
            admin.Email = "mbenegui@gmail.com";

            var adminUser = UserManager.Create(admin, userPWD);

            //Add default User to Role Admin   
            if (adminUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(admin.Id, "Admin");

            }

            var staff = new ApplicationUser();
            staff.UserName = "staff@test.com";
            staff.Email = "staff@test.com";

            var staffUser = UserManager.Create(staff, userPWD);

            //Add default User to Role Staff   
            if (staffUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(staff.Id, "Staff");

            }

            var manager = new ApplicationUser();
            manager.UserName = "manager@test.com";
            manager.Email = "manager@test.com";

            var managerUser = UserManager.Create(manager, userPWD);

            //Add default User to Role Staff   
            if (managerUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(manager.Id, "Manager");

            }

            var parent = new ApplicationUser();
            parent.UserName = "parent@test.com";
            parent.Email = "parent@test.com";

            var parentUser = UserManager.Create(parent, userPWD);

            //Add default User to Role Staff   
            if (parentUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(parent.Id, "Parent");

            }


        }
    }
}