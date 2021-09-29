using FacultyApplication.Interfaces;
using FacultyApplication.Models;
using Studenti.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacultyApplication.DAL
{
    public class StudentGroupSQL : BaseDALSQL, IStudentGroupDAL
    {
        public void Add(StudentGroup studentGroup)
        {
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand commnand = new SqlCommand("INSERT INTO StudentGroups(Name,Year,Description,FacultyId) VALUES(@Name,@Year,@Description,@FacultyId)", connection);
                connection.Open();
                commnand.Parameters.AddWithValue("@Name", studentGroup.Name);
                commnand.Parameters.AddWithValue("@Year", studentGroup.Year);
                commnand.Parameters.AddWithValue("@Description", studentGroup.Description);
                commnand.Parameters.AddWithValue("@FacultyId", studentGroup.FacultyId);
                //-- facultyID
                //
                commnand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool Delete(int stgroupId)
        {

            IStudentDAL stundetSql = new StudentSQL();
            List<Student> allStudents = stundetSql.GetStudents();
            bool result = false;


            if (!allStudents.Exists(x => x.StudentGroupId == stgroupId))
            {
                result = true;
                SqlConnection connection = BaseDALSQL.GetConnection();

                using (connection)
                {


                    SqlCommand command = new SqlCommand("DELETE FROM StudentGroups WHERE Id = @stgroupId", connection);

                    connection.Open();
                    command.Parameters.Add("@stgroupId", SqlDbType.Int).Value = stgroupId;
                    command.ExecuteNonQuery();

                    connection.Close();

                }
            }

            return result;

        }

        public List<StudentGroup> GetStudentGroups()
        {
            List<StudentGroup> studentgroups = new List<StudentGroup>();


            // string connectionstring= ""Data Source = MSSQL1; Initial Catalog = AdventureWorks; " + "Integrated Security=true;"; "

            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT *, Faculties.Name AS Faculty FROM StudentGroups INNER JOIN Faculties ON StudentGroups.FacultyId=Faculties.Id;", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        studentgroups.Add(new StudentGroup
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Year = (int)reader["Year"],
                            Description = (string)reader["Description"],
                            Faculty = (string)reader["Faculty"],
                            FacultyId = (int)reader["FacultyId"]
                            // TODO get faculty
                            // Faculty = "Cibernetica Economica"
                        });
                    }
                }

                reader.Close();
            }
            return studentgroups;
        }

        public void Update(StudentGroup studentGroup)
        {
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("UPDATE StudentGroups SET Name=@Name,Year=@Year,Description=@Description,FacultyId=@FacultyId WHERE Id=@studentgroupId;", connection);
                connection.Open();

                command.Parameters.AddWithValue("@studentgroupId", studentGroup.Id);
                command.Parameters.AddWithValue("@Year", studentGroup.Year);
                command.Parameters.AddWithValue("@Name", studentGroup.Name);
                command.Parameters.AddWithValue("@Description", studentGroup.Description);
                command.Parameters.AddWithValue("@FacultyId", studentGroup.FacultyId);

                command.ExecuteNonQuery();

                connection.Close();
            }

        }
    }
}