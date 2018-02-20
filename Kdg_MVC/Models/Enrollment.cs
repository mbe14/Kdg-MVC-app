using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kdg_MVC.Models
{

    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentID { get; set; }

        [Display(Name = "Child's Name")]
        public int CID { get; set; }

        [Display(Name = "Group")]
        public int GroupID { get; set; }

        [Display(Name = "Instructor's Name")]
        public int InstructorID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }
                
        public virtual Group Groups { get; set; }
        public virtual Children Children { get; set; }
        public virtual Instructor Instructors { get; set; }

    }
}