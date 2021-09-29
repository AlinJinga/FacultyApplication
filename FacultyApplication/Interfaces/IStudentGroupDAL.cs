using FacultyApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacultyApplication.Interfaces
{
    public interface IStudentGroupDAL
    {
        List<StudentGroup> GetStudentGroups();
        void Add(StudentGroup studentGroup);
        void Update(StudentGroup studentGroup);
        bool Delete(int stgroupId);
    }
}