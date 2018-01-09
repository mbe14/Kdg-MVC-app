using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kdg_MVC.Models
{
    public class DailyAttendance
    {
        [Key]
        public int AttendanceID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date (MM/DD/YYYY)")]
        public DateTime Att_Date { get; set; }       
       
        [Display(Name = "Child's Name")]
        public int CID { get; set; }

        [Display(Name = "Attended courses")]
        public bool isPresent { get; set; }
        public string Notes { get; set; }
        public virtual Children Children { get; set; }      

    }
}