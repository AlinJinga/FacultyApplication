using FacultyApplication.Models;
using Studenti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacultyApplication.Interfaces
{
    public interface IStudentDAL
    {
        List<Student> GetStudents();

        void Add(Student student);

        void Delete(int studentId);

        void Update(Student student);
    }
}