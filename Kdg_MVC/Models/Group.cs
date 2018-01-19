using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kdg_MVC.Models
{
    public class Group
    {
        public int GroupID { get; set; }

        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
      
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}