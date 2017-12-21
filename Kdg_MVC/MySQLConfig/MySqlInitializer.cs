using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Kdg_MVC.Models;

namespace Kdg_MVC.MySQLConfig
{
    public class MySqlInitializer : IDatabaseInitializer<ApplicationDbContext>
    {
        public void InitializeDatabase(ApplicationDbContext context)
        {
             

            if (!context.Database.Exists())
            {
                // if database did not exist before - create it
                context.Database.Create();

                context.Database.ExecuteSqlCommand("CREATE TABLE `Children` (`CID` int(4) NOT NULL AUTO_INCREMENT, `FirstName` varchar(50) NOT NULL, `LastName` varchar(50) NOT NULL, `CNP` varchar(13) NOT NULL UNIQUE, `Address` varchar(50) NOT NULL, `City` varchar(50) NOT NULL, `MothersName` varchar(100) NOT NULL, `FathersName` varchar(100) NOT NULL, `ContactEMail` varchar(50) NOT NULL, `EnrollmentDate` DATE NOT NULL, PRIMARY KEY(`cid`));");

                context.Database.ExecuteSqlCommand("CREATE TABLE `Enrollment` (`EnrollmentID` int(4) NOT NULL AUTO_INCREMENT, `GroupID` int(4) NOT NULL, `InstructorID` int(4) NOT NULL, `CID` int(4) NOT NULL, `Grades` varchar(20) NOT NULL, PRIMARY KEY (`EnrollmentID`));");

                context.Database.ExecuteSqlCommand("CREATE TABLE `Group` (`GroupID` int(4) NOT NULL AUTO_INCREMENT, `GroupName` varchar(50) NOT NULL, `MonthlyFee` DECIMAL(10, 2) NOT NULL, PRIMARY KEY(`GroupID`));");

                context.Database.ExecuteSqlCommand("CREATE TABLE `Instructor` (`InstructorID` int(4) NOT NULL AUTO_INCREMENT, `FirstName` varchar(50) NOT NULL, `LastName` varchar(50) NOT NULL, `StartDate` DATE NOT NULL, `EndDate` DATE, `PayRate` DECIMAL(10,2) NOT NULL, PRIMARY KEY (`InstructorID`));");

                context.Database.ExecuteSqlCommand("CREATE TABLE `DailyAttendance` (`AttendanceID` int(4) NOT NULL AUTO_INCREMENT, `Date` DATE NOT NULL, `CID` int(4) NOT NULL, `InstructorID` int(4) NOT NULL, `isPresent` bool NOT NULL, `Notes` varchar(200) NOT NULL, PRIMARY KEY (`AttendanceID`));");

                context.Database.ExecuteSqlCommand("ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk0` FOREIGN KEY (`GroupID`) REFERENCES `Group`(`GroupID`);");

                context.Database.ExecuteSqlCommand("ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk1` FOREIGN KEY (`InstructorID`) REFERENCES `Instructor`(`InstructorID`);");

                context.Database.ExecuteSqlCommand("ALTER TABLE `Enrollment` ADD CONSTRAINT `Enrollment_fk2` FOREIGN KEY (`CID`) REFERENCES `Children`(`cid`);");

                context.Database.ExecuteSqlCommand("ALTER TABLE `DailyAttendance` ADD CONSTRAINT `DailyAttendance_fk0` FOREIGN KEY (`CID`) REFERENCES `Children`(`cid`);");

                context.Database.ExecuteSqlCommand("ALTER TABLE `DailyAttendance` ADD CONSTRAINT `DailyAttendance_fk1` FOREIGN KEY (`InstructorID`) REFERENCES `Instructor`(`InstructorID`);");
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
        }
    }
}