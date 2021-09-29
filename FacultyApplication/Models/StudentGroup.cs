using Studenti.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FacultyApplication.Models
{
    public class StudentGroup : BaseEntity
    {   [Required(ErrorMessage ="Please enter a group name.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please enter a description.")]
        public string Description { get; set; }
        
        public string Faculty { get; set; }
        [Required(ErrorMessage ="Please enter a year.")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Please select a faculty.")]
        public int FacultyId { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

    }
}