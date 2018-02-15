using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kdg_MVC.ViewModels
{
    public class PaymentsList
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public decimal Amount { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Payment Type")]
        public string FeeType { get; set; }
    }
}