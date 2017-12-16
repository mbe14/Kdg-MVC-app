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

                context.Database.ExecuteSqlCommand("CREATE TABLE `Group` (`GroupID` int(4) NOT NULL AUTO_INCREMENT, `GroupName` varchar(50) NOT NULL, `MonthlyFee` DECIMAL(10, 2) NOT NULL, PRIMARY KEY(`GroupID`));");
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