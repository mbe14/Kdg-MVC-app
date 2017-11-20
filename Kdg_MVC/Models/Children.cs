using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Kdg_MVC.Models
{
    public class Children
    {
        [Key]
        public int CID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNP { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string MothersName { get; set; }
        public string FathersName { get; set; }
        public string ContactEmail { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}