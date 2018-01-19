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
        public static void CreateDataTables(ApplicationDbContext context)
        {
            context.Database.ExecuteSqlCommand(SeedData._CreateTbChildren);
            context.Database.ExecuteSqlCommand(SeedData._CreateTbEnrollment);
            context.Database.ExecuteSqlCommand(SeedData._CreateTbGroup);
            context.Database.ExecuteSqlCommand(SeedData._CreateTbInstructor);
            context.Database.ExecuteSqlCommand(SeedData._CreateTbDailyAttendance);
            context.Database.ExecuteSqlCommand(SeedData._CreateTbPayments);
            context.Database.ExecuteSqlCommand(SeedData._CreateTbFeeTypes);
            context.Database.ExecuteSqlCommand(SeedData._Add_Enrollment_fk0);
            context.Database.ExecuteSqlCommand(SeedData._Add_Enrollment_fk1);
            context.Database.ExecuteSqlCommand(SeedData._Add_Enrollment_fk2);
            context.Database.ExecuteSqlCommand(SeedData._Add_DailyAttendance_fk0);
            context.Database.ExecuteSqlCommand(SeedData._Add_Payments_fk0);
            context.Database.ExecuteSqlCommand(SeedData._Add_Payments_fk1);
        }
        public static void CreateUsersAndRoles(ApplicationDbContext context)
        {
            SeedData.AddUserRoles(context);
            SeedData.SeedUsersToRoles(context);
        }
    }
}
