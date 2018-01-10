using Kdg_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kdg_MVC.MySQLConfig.lib
{
    public class SeedData
    {     
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
    }
}