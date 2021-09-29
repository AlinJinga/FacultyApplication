using FacultyApplication.Interfaces;
using FacultyApplication.Models;
using Studenti.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FacultyApplication.DAL
{
    public class StudentSQL : BaseDALSQL, IStudentDAL
    {
        public void Add(Student student)
        {
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("INSERT INTO Students(FirstName,LastName,Address,Sex,DateofBirth,Description,StudentGroupId) VALUES(@FirstName,@LastName,@Address,@Sex,@DateofBirth,@Description,@StudentGroupId)", connection);


                connection.Open();

                // command.Parameters.AddWithValue("@Id", student.Id);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Address", student.Address);

                if (student.Sex == "M")
                {
                    command.Parameters.AddWithValue("@Sex", true);
                }
                else
                {
                    command.Parameters.AddWithValue("@Sex", false);
                }

                command.Parameters.AddWithValue("@DateofBirth", student.DateofBirth);
                command.Parameters.AddWithValue("@Description", student.Description);
                command.Parameters.AddWithValue("@StudentGroupId", student.StudentGroupId);



                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Delete(int studentId)
        {
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("DELETE FROM Students WHERE Id = @studentId", connection);
                command.Parameters.Add("@studentId", SqlDbType.Int).Value = studentId;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();



            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT Students.* ,StudentGroups.Name AS StudentGroup, Faculties.Name AS Faculty FROM Students INNER JOIN StudentGroups " +
                    " ON Students.StudentGroupId = StudentGroups.Id INNER JOIN Faculties ON StudentGroups.FacultyId = Faculties.Id", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        students.Add(new Student
                        {
                            Id = (int)reader["id"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Sex = reader["Sex"].ToString(),
                            Address = (string)reader["Address"],
                            DateofBirth = (DateTime)reader["DateofBirth"],
                            Description = (string)reader["Description"],
                            StudentGroupId = (int)reader["StudentGroupId"],
                            StudentGroup=(string)reader["StudentGroup"],
                            Faculty=(string)reader["Faculty"]

                        });


                    }
                }

                reader.Close();
            }
            return students;
        }



        public void Update(Student student)
        {
            SqlConnection connection = BaseDALSQL.GetConnection();

            using (connection)
            {
                SqlCommand command = new SqlCommand("UPDATE Students SET FirstName = @FirstName,LastName=@LastName,Address=@Address,Sex=@Sex,DateofBirth=@DateofBirth,Description=@Description,StudentGroupId=@StudentGroupId WHERE Id=@studentId;", connection);
                connection.Open();

                command.Parameters.AddWithValue("@studentId", student.Id);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Address", student.Address);
                if (student.Sex == "M")
                {
                    command.Parameters.AddWithValue("@Sex", true);
                }

                else
                {
                    command.Parameters.AddWithValue("@Sex", false);
                }

                command.Parameters.AddWithValue("@DateofBirth", student.DateofBirth);
                command.Parameters.AddWithValue("@Description", student.Description);
                command.Parameters.AddWithValue("@StudentGroupId", student.StudentGroupId);



                command.ExecuteNonQuery();

                connection.Close();
            }

        }


    }
}