using FacultyApplication.Interfaces;
using FacultyApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacultyApplication.DAL
{
    public class FacultySQL : BaseDALSQL, IFacultyDAL
    {
        public List<Faculty> GetFaculties()
        {
            List<Faculty> faculties = new List<Faculty>();

            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Faculties;", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        faculties.Add(new Faculty
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["Name"],
                            FoundingYear = (int)reader["FoundingYear"],
                            Description = (string)reader["Description"]

                        });


                    }
                }

                reader.Close();
            }
            return faculties;
        }

        public void Add(Faculty faculty)
        {
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand commnand = new SqlCommand("INSERT INTO Faculties(Name,FoundingYear,Description) VALUES(@Name,@FoundingYear,@Description)", connection);
                connection.Open();
                commnand.Parameters.AddWithValue("@Name", faculty.Name);
                commnand.Parameters.AddWithValue("@FoundingYear", faculty.FoundingYear);
                commnand.Parameters.AddWithValue("@Description", faculty.Description);
                commnand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool Delete(int facultyId)
        {
            IStudentGroupDAL studentGroupSql = new StudentGroupSQL();
            List<StudentGroup> allGroups = studentGroupSql.GetStudentGroups();
            bool result = false;

            if (!allGroups.Exists(x => x.FacultyId == facultyId))
            {
                result = true;
                SqlConnection connection = BaseDALSQL.GetConnection();

                using (connection)
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Faculties WHERE Id=@facultyId", connection);

                    connection.Open();
                    command.Parameters.Add("@facultyId", SqlDbType.Int).Value = facultyId;

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return result;
        }

        public void Update(Faculty faculty)
        {
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("UPDATE Faculties SET Name=@Name,FoundingYear=@FoundingYear,Description=@Description WHERE Id=@facultyId;", connection);
                connection.Open();

                command.Parameters.AddWithValue("@facultyId", faculty.Id);
                command.Parameters.AddWithValue("@Name", faculty.Name);
                command.Parameters.AddWithValue("@FoundingYear", faculty.FoundingYear);
                command.Parameters.AddWithValue("@Description", faculty.Description);



                command.ExecuteNonQuery();

                connection.Close();
            }

        }

    }
}