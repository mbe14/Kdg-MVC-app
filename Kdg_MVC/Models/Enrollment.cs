using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Child's Name")]
        public int CID { get; set; }

        [Display(Name = "Group")]
        public int GroupID { get; set; }

        [Display(Name = "Instructor's Name")]
        public int InstructorID { get; set; }
        
        public Grade? Grades { get; set; }
        
        public virtual Group Groups { get; set; }
        public virtual Children Children { get; set; }
        public virtual Instructor Instructors { get; set; }

    }
}