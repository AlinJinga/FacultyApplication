using Studenti.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FacultyApplication.Models
{
    /// <summary>
    /// Student model
    /// </summary>
    public class Student : BaseEntity
    {   [Required(ErrorMessage ="Please enter first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name.")]
        public string LastName { get; set; }

        public int Age
        {
            get
            {

                return (int)Math.Floor(DateTime.Now.Subtract(this.DateofBirth).TotalDays / 365);


            }
        }
        [Required(ErrorMessage = "Please select a sex.")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Please enter an address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please select date of birth.")]
        public DateTime DateofBirth { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }
      
        public int StudentGroupId { get; set; }

        public string Faculty { get; set; }

        public string StudentGroup { get; set; }

        public int FacultyId { get; set; }
    }
}
