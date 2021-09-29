using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FacultyApplication.DAL;
using FacultyApplication.Models;

namespace FacultyApplication.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddStudent(int? studentId)
        {
            ViewBag.Message = "Your contact page.";

            SetUpFaculties();
            Student st = new Student();


            if (studentId.HasValue && studentId > 0)
            {
                st = new StudentSQL().GetStudents().Find(x => x.Id == studentId);

            }

            int firstFacultyId = SetUpFaculties();
            SetUpStudentGroups(firstFacultyId);

            return View(st);
        }


        public ActionResult Faculties()
        {
            List<Faculty> faculties = new List<Faculty>();
            FacultySQL facultySQL = new FacultySQL();
            faculties = facultySQL.GetFaculties();
            GetGroupsNumber(faculties);


            return View(faculties);
        }

        public ActionResult Students()
        {
            List<Student> students = new List<Student>();
            StudentSQL studentSQL = new StudentSQL();
            students = studentSQL.GetStudents();

            return View(students);
        }

        public ActionResult StudentGroups()
        {
            List<StudentGroup> studentGroups = new List<StudentGroup>();
            StudentGroupSQL studentGroupSQL = new StudentGroupSQL();
            studentGroups = studentGroupSQL.GetStudentGroups();

            return View(studentGroups);
        }

        [HttpPost]
        public ActionResult AddStudent(FacultyApplication.Models.Student student)
        {
            StudentSQL studentSQL = new StudentSQL();
            SetUpFaculties();
            SetUpStudentGroups(student.FacultyId);


            if (ModelState.IsValid)
            {
                if (student.Id == 0)
                {
                    studentSQL.Add(student);
                }
                else
                {
                    studentSQL.Update(student);
                }
            }

            return View();
        }

        public ActionResult AddFaculty(int? facultyId)
        {
            ViewBag.Message = "Your contact page.";

            Faculty faculty = new Faculty();

            if (facultyId.HasValue && facultyId > 0)
            {
                faculty = new FacultySQL().GetFaculties().Find(x => x.Id == facultyId);

            }

            return View(faculty);
        }
        [HttpPost]
        public ActionResult AddFaculty(Faculty faculty)
        {
            FacultySQL facultySQL = new FacultySQL();
            if (ModelState.IsValid)
            {
                if (faculty.Id == 0)
                {
                    facultySQL.Add(faculty);

                }
                else
                {
                    facultySQL.Update(faculty);
                }


            }


            return View();
        }

        public ActionResult AddStudentGroup(int? studentGroupId)
        {
            SetUpFaculties();
            StudentGroup studentGroup = new StudentGroup();

            if (studentGroupId.HasValue && studentGroupId > 0)
            {
                studentGroup = new StudentGroupSQL().GetStudentGroups().Find(x => x.Id == studentGroupId);
            }

            return View(studentGroup);
        }

        private int SetUpFaculties()
        {
            ViewBag.Message = "Your contact page.";
            List<SelectListItem> facultiesItems = new List<SelectListItem>();

            List<Faculty> faculties = new List<Faculty>();
            FacultySQL facultySQL = new FacultySQL();
            faculties = facultySQL.GetFaculties();
            foreach (Faculty faculty in faculties)
            {

                facultiesItems.Add(new SelectListItem
                {
                    Value = faculty.Id.ToString(),
                    Text = faculty.Name
                });

            }

            ViewBag.Faculties = facultiesItems;

            return faculties[0].Id;

        }

        private void SetUpStudentGroups(int facultyId)
        {
            ViewBag.Message = "Your contact page.";
            List<SelectListItem> studentGroupsItems = new List<SelectListItem>();

            List<StudentGroup> studentGroups = new List<StudentGroup>();
            StudentGroupSQL studentGroupSQL = new StudentGroupSQL();
            studentGroups = studentGroupSQL.GetStudentGroups().FindAll(x => x.FacultyId == facultyId);

            foreach (StudentGroup studentGroup in studentGroups)
            {

                studentGroupsItems.Add(new SelectListItem
                {
                    Value = studentGroup.Id.ToString(),
                    Text = studentGroup.Name
                });


            }

            ViewBag.StudentGroups = studentGroupsItems;
        }



        [HttpGet]
        public JsonResult GetFacultiesGroup(int facultyId)
        {
            StudentGroupSQL studentGroupSQL = new StudentGroupSQL();
            var studentGroups = studentGroupSQL.GetStudentGroups().FindAll(x => x.FacultyId == facultyId);


            return Json(studentGroups, JsonRequestBehavior.AllowGet);
        }

        public void GetGroupsNumber(List<Faculty> faculties)
        {
           // List<Faculty> faculties = new List<Faculty>();
           // faculties = new FacultySQL().GetFaculties();
            List<StudentGroup> studentGroups = new StudentGroupSQL().GetStudentGroups();
            
            foreach(Faculty faculty in faculties)
            {
                faculty.studentGroupsNumber= studentGroups.FindAll(x => x.FacultyId == faculty.Id).Count();
            }

        }


        //[HttpPost]
        public JsonResult DeleteStudents(int studentId)
        {
            StudentSQL studentSQL = new StudentSQL();

            studentSQL.Delete(studentId);


            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFaculties(int facultyId)
        {
            FacultySQL facultySQL = new FacultySQL();
            bool result = facultySQL.Delete(facultyId);
            facultySQL.Delete(facultyId);
            if (result)
            {
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("This faculty can't be deleted", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteStudentGroups(int studentGroupId)
        {
            StudentGroupSQL studentGroupSQL = new StudentGroupSQL();
            bool result = studentGroupSQL.Delete(studentGroupId);
            studentGroupSQL.Delete(studentGroupId);

            if (result)
            {
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("This group can't be deleted", JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult AddStudentGroup(FacultyApplication.Models.StudentGroup studentGroup)
        {
            StudentGroupSQL studentGroupSQL = new StudentGroupSQL();
            SetUpFaculties();

            if (ModelState.IsValid)
            {
                if (studentGroup.Id == 0)
                {
                    studentGroupSQL.Add(studentGroup);
                }
                else
                {
                    studentGroupSQL.Update(studentGroup);
                }

            }


            return View();
        }

    }
}