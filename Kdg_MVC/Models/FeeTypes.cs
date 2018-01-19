using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kdg_MVC.Models
{
    public class FeeTypes
    {
       [Key]
        public int FeeID { get; set; }

        public string FeeType { get; set; }
    }
}