using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kdg_MVC.Models
{
    public class Payments
    {
        [Key]
        public int PaymentID { get; set; }

        [Required]
        public int CID { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
       
        [Required]
        public int FeeID { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime Date { get; set; }

        public virtual Children Children { get; set; }
        public virtual FeeTypes FeeTypes { get; set; }
    }
}