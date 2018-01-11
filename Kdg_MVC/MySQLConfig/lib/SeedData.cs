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
        public const string _CreateTbChildren = @"CREATE TABLE `Children` (`CID` int(4) NOT NULL AUTO_INCREMENT, `FirstName` varchar(50) NOT NULL, `LastName` varchar(50) NOT NULL, `CNP` varchar(13) NOT NULL UNIQUE, `Address` varchar(50) NOT NULL, `City` varchar(50) NOT NULL, `MothersName` varchar(100) NOT NULL, `FathersName` varchar(100) NOT NULL, `ContactEMail` varchar(50) NOT NULL, `EnrollmentDate` DATE NOT NULL, PRIMARY KEY(`cid`));";

        public const string _CreateTbEnrollment = @"CREATE TABLE `Enrollment` (`EnrollmentID` int(4) NOT NULL AUTO_INCREMENT, `GroupID` int(4) NOT NULL, `InstructorID` int(4) NOT NULL, `CID` int(4) NOT NULL, `Grades` varchar(20) NOT NULL, PRIMARY KEY (`EnrollmentID`));";

        public const string _CreateTbGroup = @"CREATE TABLE `Group` (`GroupID` int(4) NOT NULL AUTO_INCREMENT, `GroupName` varchar(50) NOT NULL, `MonthlyFee` DECIMAL(10, 2) NOT NULL, PRIMARY KEY(`GroupID`));";

        public const string _CreateTbInstructor = @"CREATE TABLE `Instructor` (`InstructorID` int(4) NOT NULL AUTO_INCREMENT, `FirstName` varchar(50) NOT NULL, `LastName` varchar(50) NOT NULL, `StartDate` DATE NOT NULL, `EndDate` DATE, `PayRate` DECIMAL(10,2) NOT NULL, PRIMARY KEY (`InstructorID`));";

        public const string _CreateTbDailyAttendance = @"CREATE TABLE `DailyAttendance` (`AttendanceID` int(4) NOT NULL AUTO_INCREMENT, `Att_Date` DATE NOT NULL, `CID` int(4) NOT NULL, `isPresent` bool NOT NULL, `Notes` varchar(200), PRIMARY KEY (`AttendanceID`));";

        public const string _Add_Enrollment_fk0 = @"ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk0` FOREIGN KEY (`GroupID`) REFERENCES `Group`(`GroupID`);";

        public const string _Add_Enrollment_fk1 = @"ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk1` FOREIGN KEY (`InstructorID`) REFERENCES `Instructor`(`InstructorID`);";

        public const string _Add_Enrollment_fk2 = @"ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk2` FOREIGN KEY (`CID`) REFERENCES `Children`(`cid`);";

        public const string _Add_DailyAttendance_fk0 = @"ALTER TABLE `DailyAttendance` ADD CONSTRAINT `DailyAttendance_fk0` FOREIGN KEY (`CID`) REFERENCES `Children`(`cid`);";
        
        public static void Seed()
        {
            Kdg_MVC.DataAccessLayer.AppContext ctx = new DataAccessLayer.AppContext();
            var chldren = new List<Children>
            {
                new Children
                {
                    FirstName = "Jane", LastName = "Austen", CNP = "2021001125899", Address = "Some Address", City = "Some City", MothersName = "Amanda", FathersName = "John", ContactEmail = "austen@test.com", EnrollmentDate = Convert.ToDateTime("01/01/2017") // Month / Day / Year
                },
                new Children
                {
                    FirstName = "Mike", LastName = "Jensen", CNP = "1030401138412", Address = "Some Address", City = "Some City", MothersName = "Lucy", FathersName = "Jack", ContactEmail = "austen@test.com", EnrollmentDate = Convert.ToDateTime("01/01/2017")
                },
                new Children
                {
                    FirstName = "James", LastName = "Cameron", CNP = "1021201152854", Address = "Some Address", City = "Some City", MothersName = "Stacey", FathersName = "Gunther", ContactEmail = "austen@test.com", EnrollmentDate = Convert.ToDateTime("01/01/2017")
                }
            };

            chldren.ForEach(s => ctx.Children.Add(s));

            var instrcts = new List<Instructor>
            {
                new Instructor
                {
                    FirstName = "Mike", LastName="Johnson", PayRate=1200.2M, StartDate=Convert.ToDateTime("12/11/2012")
                },
                new Instructor
                {
                    FirstName = "Sam", LastName="Smith", PayRate=1200.2M, StartDate=Convert.ToDateTime("12/11/2012")
                },
                 new Instructor
                {
                    FirstName = "Lucy", LastName="Liu", PayRate=1200.2M, StartDate=Convert.ToDateTime("12/11/2012")
                }
            };

            instrcts.ForEach(s => ctx.Instructors.Add(s));

            var grps = new List<Group>
            {
                new Group
                {
                    GroupName = "Grupa Mica", MonthlyFee=900M
                },
                new Group
                {
                    GroupName = "Grupa Mare", MonthlyFee=950M
                },
                new Group
                {
                    GroupName = "Licuricii", MonthlyFee=925M
                },
                new Group
                {
                    GroupName = "Grupa Mijlocie", MonthlyFee=915M
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