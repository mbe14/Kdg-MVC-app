using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kdg_MVC.ViewModels
{
    public class EnrollmentData
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Attendance Date")]
        public DateTime Date { get; set; }

        public string GroupName { get; set; }
    }
}