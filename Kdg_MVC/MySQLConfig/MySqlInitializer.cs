using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Kdg_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Kdg_MVC.MySQLConfig.lib;

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
                
                //Create Data Tables
                DatabaseRepository.CreateDataTables(context);

                //Create users and roles.
                DatabaseRepository.CreateUsersAndRoles(context);
                
                //Start Seeding the Database;
                SeedData.Seed();                               
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
                    DatabaseRepository.CreateDataTables(context);
                    DatabaseRepository.CreateUsersAndRoles(context);
                    SeedData.Seed();
                }
            }            
        }
    }
}