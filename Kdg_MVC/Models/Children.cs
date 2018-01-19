using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace Kdg_MVC.Models
{
    public class Children
    {
        [Key]
        public int CID { get; set; }
        
        [Required]
        [Column("FirstName")]
        [Display(Name="First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [Column("LastName")]
        [Display(Name="Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }
        
        [Required]
        [Display(Name="CNP")]
        public string CNP { get; set; }

        [Display(Name="Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Mother's Full Name")]
        public string MothersName { get; set; }

        [Display(Name = "Father's Full Name")]
        public string FathersName { get; set; }
        
        [Required]
        [Display(Name = "Contact E-Mail")]
        public string ContactEmail { get; set; }
                     
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        [Display(Name = "Child's Full Name")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Gender
        {                      
            get
            {                
                var chars = CNP.ToCharArray();
                
                if (chars[0] == '1')
                {
                    return "Male";
                }

                else
                {
                    return "Female";
                }

            }
        }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DOB
        {
            get
            {
                string year = ("20" + CNP[1] + CNP[2]);
                string month = (CNP[3].ToString() + CNP[4].ToString());
                string day = (CNP[5].ToString() + CNP[6].ToString());

                string new_dob = day + "-" + month + "-" + year;

                return DateTime.ParseExact(new_dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            }
        }
    }
}