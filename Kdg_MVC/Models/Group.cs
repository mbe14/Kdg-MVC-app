using System;
using System.Collections.Generic;

namespace Kdg_MVC.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public Decimal MonthlyFee { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}