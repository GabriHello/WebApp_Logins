using Nome.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Nome.Controllers
{
    internal class StudentsService
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringLocal"].ConnectionString;
        internal List<Student> getStudentsList()
        {
            List<Student> students = new List<Student>();
            var queryString = "SELECT studentId, firstname, lastname FROM students ORDER BY studentId ASC";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read()) //eseguo una lettura finchè ci sono record da visionare
                {
                    students.Add(new Student
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("studentId")),
                        Firstname = reader.GetString(reader.GetOrdinal("firstname")),
                        Lastname = reader.GetString(reader.GetOrdinal("lastname"))
                    });
                }
            }
            return students;
        }
        internal int InsertAStudent(StudentToInsert studentInserted)
        {
            var counter = 0;
            var queryString = "INSERT INTO students (firstname,lastname) values (@fn, @ln)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                cmd.Parameters.Add("@fn", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@fn"].Value = studentInserted.FirstName; 
                cmd.Parameters.Add("@ln", System.Data.SqlDbType.VarChar);
                cmd.Parameters["@ln"].Value = studentInserted.LastName;

                counter = cmd.ExecuteNonQuery();
            }
            return counter;
        }
    }
}