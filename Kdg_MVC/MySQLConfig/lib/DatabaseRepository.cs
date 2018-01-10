using Kdg_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kdg_MVC.MySQLConfig.lib
{
    public static class DatabaseRepository
    {
        private const string _CreateTbChildren = @"CREATE TABLE `Children` (`CID` int(4) NOT NULL AUTO_INCREMENT, `FirstName` varchar(50) NOT NULL, `LastName` varchar(50) NOT NULL, `CNP` varchar(13) NOT NULL UNIQUE, `Address` varchar(50) NOT NULL, `City` varchar(50) NOT NULL, `MothersName` varchar(100) NOT NULL, `FathersName` varchar(100) NOT NULL, `ContactEMail` varchar(50) NOT NULL, `EnrollmentDate` DATE NOT NULL, PRIMARY KEY(`cid`));";

        private const string _CreateTbEnrollment = @"CREATE TABLE `Enrollment` (`EnrollmentID` int(4) NOT NULL AUTO_INCREMENT, `GroupID` int(4) NOT NULL, `InstructorID` int(4) NOT NULL, `CID` int(4) NOT NULL, `Grades` varchar(20) NOT NULL, PRIMARY KEY (`EnrollmentID`));";

        private const string _CreateTbGroup = @"CREATE TABLE `Group` (`GroupID` int(4) NOT NULL AUTO_INCREMENT, `GroupName` varchar(50) NOT NULL, `MonthlyFee` DECIMAL(10, 2) NOT NULL, PRIMARY KEY(`GroupID`));";

        private const string _CreateTbInstructor = @"CREATE TABLE `Instructor` (`InstructorID` int(4) NOT NULL AUTO_INCREMENT, `FirstName` varchar(50) NOT NULL, `LastName` varchar(50) NOT NULL, `StartDate` DATE NOT NULL, `EndDate` DATE, `PayRate` DECIMAL(10,2) NOT NULL, PRIMARY KEY (`InstructorID`));";

        private const string _CreateTbDailyAttendance = @"CREATE TABLE `DailyAttendance` (`AttendanceID` int(4) NOT NULL AUTO_INCREMENT, `Att_Date` DATE NOT NULL, `CID` int(4) NOT NULL, `isPresent` bool NOT NULL, `Notes` varchar(200), PRIMARY KEY (`AttendanceID`));";

        private const string _Add_Enrollment_fk0 = @"ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk0` FOREIGN KEY (`GroupID`) REFERENCES `Group`(`GroupID`);";

        private const string _Add_Enrollment_fk1 = @"ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk1` FOREIGN KEY (`InstructorID`) REFERENCES `Instructor`(`InstructorID`);";

        private const string _Add_Enrollment_fk2 = @"ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk2` FOREIGN KEY (`CID`) REFERENCES `Children`(`cid`);";

        private const string _Add_DailyAttendance_fk0 = @"ALTER TABLE `DailyAttendance` ADD CONSTRAINT `DailyAttendance_fk0` FOREIGN KEY (`CID`) REFERENCES `Children`(`cid`);";

        public static void CreateDataTables(ApplicationDbContext context)
        {           
            context.Database.ExecuteSqlCommand(_CreateTbChildren);
            context.Database.ExecuteSqlCommand(_CreateTbEnrollment);
            context.Database.ExecuteSqlCommand(_CreateTbGroup);
            context.Database.ExecuteSqlCommand(_CreateTbInstructor);
            context.Database.ExecuteSqlCommand(_CreateTbDailyAttendance);
            context.Database.ExecuteSqlCommand(_Add_Enrollment_fk0);
            context.Database.ExecuteSqlCommand(_Add_Enrollment_fk1);
            context.Database.ExecuteSqlCommand(_Add_Enrollment_fk2);
            context.Database.ExecuteSqlCommand(_Add_DailyAttendance_fk0);
        }


        public static void CreateUsersAndRoles(ApplicationDbContext context)
        {          
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Creating First Admin role and a default admin user
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "mbenegui@gmail.com";
                user.Email = "mbenegui@gmail.com";
                string userPWD = "!Pass123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
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
        }
    }
}