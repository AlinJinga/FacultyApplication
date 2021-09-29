using Studenti.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FacultyApplication.Models
{
    public class Faculty : BaseEntity
    {   [Required(ErrorMessage ="Please enter a faculty name.")]    
        public string Name { get; set; }
        [Range(1950,2021,ErrorMessage ="Please enter a valid value.")]
        [Required(ErrorMessage ="Please enter a value.")]
        public int FoundingYear { get; set; }
        [Required(ErrorMessage ="Please enter a description.")]
        public string Description { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public int studentGroupsNumber { get; set; }

    }
}