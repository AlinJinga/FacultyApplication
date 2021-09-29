using FacultyApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacultyApplication.Interfaces
{
    public interface IFacultyDAL
    {
        List<Faculty> GetFaculties();

        bool Delete(int facultyId);


        void Add(Faculty faculty);


        void Update(Faculty faculty);

    }
}