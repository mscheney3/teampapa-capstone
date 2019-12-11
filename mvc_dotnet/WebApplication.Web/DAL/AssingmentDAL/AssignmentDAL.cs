using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;

namespace WebApplication.Web.DAL.AssingmentDAL
{
    public class AssignmentDAL : IAssignmentDAL
    {
        private readonly string connectionString;
        private readonly string sql_GetAllStudents = "SELECT * FROM users WHERE role = 'Student';";
        private readonly string sql_GetAllTeachers = "SELECT * FROM users WHERE role = 'Teacher';";
        private readonly string sql_AssignStudent = "INSERT INTO teachers(teacher_id, student_id) VALUES (@teacherID, @studentID);";
        private readonly string sql_AssignScenario = "INSERT INTO students(student_id, scenario_id) VALUES (@studentID, @scenarioID);";

        public AssignmentDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<User> GetAllStudents()
        {

            IList<User> students = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = sql_GetAllStudents;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new User
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Username = Convert.ToString(reader["username"]),
                        Password = Convert.ToString(reader["password"]),
                        Salt = Convert.ToString(reader["salt"]),
                        Role = Convert.ToString(reader["role"])
                    });
                    
                }
                return students;
            }
        }

        public IList<User> GetAllTeachers()
        {

            IList<User> teachers = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = sql_GetAllTeachers;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    teachers.Add(new User
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Username = Convert.ToString(reader["username"]),
                        Password = Convert.ToString(reader["password"]),
                        Salt = Convert.ToString(reader["salt"]),
                        Role = Convert.ToString(reader["role"])
                    });

                }
                return teachers;
            }
        }

        public bool AssignStudent(int teacherID, int studentID)
        {
            bool result = false;
            int affected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql_AssignStudent, conn);
                cmd.Parameters.AddWithValue("@teacherID", teacherID);
                cmd.Parameters.AddWithValue("@studentID", studentID);


                affected = cmd.ExecuteNonQuery();
            }

            if (affected >= 1)
            {
                result = true;
            }

            return result;
        }

        public bool AssignScenario(int studentID, int scenarioID)
        {
            bool result = false;
            int affected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql_AssignScenario, conn);
                cmd.Parameters.AddWithValue("@studentID", studentID);
                cmd.Parameters.AddWithValue("@scenarioID", scenarioID);


                affected = cmd.ExecuteNonQuery();
            }

            if (affected >= 1)
            {
                result = true;
            }

            return result;
        }



    }
}
