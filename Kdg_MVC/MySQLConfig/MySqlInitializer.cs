using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Kdg_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kdg_MVC.MySQLConfig
{
    public class MySqlInitializer : IDatabaseInitializer<ApplicationDbContext>
    {

        Kdg_MVC.DataAccessLayer.AppContext ctx = new DataAccessLayer.AppContext();
        private void Seed()
        {
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

        public void InitializeDatabase(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Database.Exists())
            {
                // if database did not exist before - create it
                context.Database.Create();

                context.Database.ExecuteSqlCommand("CREATE TABLE `Children` (`CID` int(4) NOT NULL AUTO_INCREMENT, `FirstName` varchar(50) NOT NULL, `LastName` varchar(50) NOT NULL, `CNP` varchar(13) NOT NULL UNIQUE, `Address` varchar(50) NOT NULL, `City` varchar(50) NOT NULL, `MothersName` varchar(100) NOT NULL, `FathersName` varchar(100) NOT NULL, `ContactEMail` varchar(50) NOT NULL, `EnrollmentDate` DATE NOT NULL, PRIMARY KEY(`cid`));");

                context.Database.ExecuteSqlCommand("CREATE TABLE `Enrollment` (`EnrollmentID` int(4) NOT NULL AUTO_INCREMENT, `GroupID` int(4) NOT NULL, `InstructorID` int(4) NOT NULL, `CID` int(4) NOT NULL, `Grades` varchar(20) NOT NULL, PRIMARY KEY (`EnrollmentID`));");

                context.Database.ExecuteSqlCommand("CREATE TABLE `Group` (`GroupID` int(4) NOT NULL AUTO_INCREMENT, `GroupName` varchar(50) NOT NULL, `MonthlyFee` DECIMAL(10, 2) NOT NULL, PRIMARY KEY(`GroupID`));");

                context.Database.ExecuteSqlCommand("CREATE TABLE `Instructor` (`InstructorID` int(4) NOT NULL AUTO_INCREMENT, `FirstName` varchar(50) NOT NULL, `LastName` varchar(50) NOT NULL, `StartDate` DATE NOT NULL, `EndDate` DATE, `PayRate` DECIMAL(10,2) NOT NULL, PRIMARY KEY (`InstructorID`));");

                context.Database.ExecuteSqlCommand("CREATE TABLE `DailyAttendance` (`AttendanceID` int(4) NOT NULL AUTO_INCREMENT, `Att_Date` DATE NOT NULL, `CID` int(4) NOT NULL, `isPresent` bool NOT NULL, `Notes` varchar(200), PRIMARY KEY (`AttendanceID`));");

                context.Database.ExecuteSqlCommand("ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk0` FOREIGN KEY (`GroupID`) REFERENCES `Group`(`GroupID`);");

                context.Database.ExecuteSqlCommand("ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk1` FOREIGN KEY (`InstructorID`) REFERENCES `Instructor`(`InstructorID`);");

                context.Database.ExecuteSqlCommand("ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk2` FOREIGN KEY (`CID`) REFERENCES `Children`(`cid`);");

                context.Database.ExecuteSqlCommand("ALTER TABLE `DailyAttendance` ADD CONSTRAINT `DailyAttendance_fk0` FOREIGN KEY (`CID`) REFERENCES `Children`(`cid`);");
               
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

                // creating Creating Manager role    
                if (!roleManager.RoleExists("Staff"))
                {
                    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role.Name = "Staff";
                    roleManager.Create(role);

                }

                // creating Creating Employee role    
                if (!roleManager.RoleExists("Parent"))
                {
                    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role.Name = "Parent";
                    roleManager.Create(role);

                }
            }
            else
            {
                // query to check if MigrationHistory table is present in the database 
                var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
                  "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'IdentityMySQLDatabase' AND table_name = '__MigrationHistory'");

                // if MigrationHistory table is not there (which is the case first time we run) - create it
                if (migrationHistoryTableExists.FirstOrDefault() == 0)
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }

            //Start Seeding the Database;
            Seed();
        }
    }
}