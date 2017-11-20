using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kdg_MVC.Models
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public decimal MonthlyFree { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}