﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kdg_MVC.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Kdg_MVC.DataAccessLayer
{
    public class AppContext : DbContext
    {
        public AppContext() : base("DefaultConnection")
        {
        }
        
        public DbSet<Children>Children { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<DailyAttendance> DailyAttendances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<Kdg_MVC.Models.FeeTypes> FeeTypes { get; set; }

        public System.Data.Entity.DbSet<Kdg_MVC.Models.Payments> Payments { get; set; }

    }
}