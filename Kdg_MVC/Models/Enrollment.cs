using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kdg_MVC.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int InstructorID { get; set; }
        public int GroupID { get; set; }
        public int ChildID { get; set; }
        public Grade? Grade { get; set; }
        public virtual Group Group { get; set; }
        public virtual Children Children { get; set; }


    }
}