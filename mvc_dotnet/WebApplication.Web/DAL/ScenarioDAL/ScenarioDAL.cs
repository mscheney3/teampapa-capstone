using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models.Scenario;

namespace WebApplication.Web.DAL.ScenarioDAL
{
    public class ScenarioDAL : IScenarioDAL
    {
        private readonly string connectionString;

        private readonly string sql_GetAllUserScenarios = "SELECT * FROM scenarios JOIN students ON students.scenario_id = scenarios.scenario_id WHERE students.student_id = @studentId";
        private readonly string sql_GetUserScenarios = "SELECT * FROM scenarios WHERE scenario_id = @scenarioId";
        private readonly string sql_GetAllScenarios = "SELECT * FROM scenarios";
        private readonly string sql_GetScenarioAnswers = "SELECT * FROM answers WHERE scenario_id = @scenarioId";
        private readonly string sql_GetResponse = "SELECT * FROM answers WHERE answer_id = @answerId";
        private readonly string sql_SaveReview = "INSERT INTO review (user_id, answer_id, date_answered) VALUES (@userId, @answerId, GETDATE())";
        private readonly string sql_GetReview = "SELECT * FROM review JOIN answers ON review.answer_id = answers.answer_id " +
            "JOIN scenarios ON scenarios.scenario_id = answers.scenario_id WHERE user_id = @userId ORDER BY review.date_answered DESC";
        private readonly string sql_UpdateScenario = "Update scenarios SET isActive = @isActive WHERE scenario_id = @scenarioId";
        private readonly string sql_CreateScenario = "INSERT INTO scenarios (scenario_name, description, scenario_image, question, isActive) " +
            "VALUES (@scenarioName, @description, @image, @question, @isActive)";
        private readonly string sql_CreateAnswer = "INSERT INTO answers (scenario_id, answer_text, response_text, response_image) " +
            "VALUES (@scenarioId, @answerText, @responseText, @responseImage)";
        private readonly string sql_GetMaxScenarioId = "SELECT * FROM scenarios WHERE scenario_id = (SELECT MAX(scenario_id) FROM scenarios)";

        public ScenarioDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Scenario> GetAllUserScenarios(int studentId)
        {
            List<Scenario> scenarios = new List<Scenario>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_GetAllUserScenarios, conn);
                    cmd.Parameters.AddWithValue("@studentID", studentId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Scenario scenario = new Scenario();

                        scenario.Id = Convert.ToInt32(reader["scenario_id"]);
                        scenario.Name = Convert.ToString(reader["scenario_name"]);
                        scenario.Description = Convert.ToString(reader["description"]);
                        scenario.ImageName = Convert.ToString(reader["scenario_image"]);
                        scenario.Question = Convert.ToString(reader["question"]);
                        scenario.IsActive = Convert.ToBoolean(reader["isActive"]);

                        scenarios.Add(scenario);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (scenarios.Count == 0)
            {
                scenarios = GetAllScenarios();
            }

            return scenarios;
        }

        public Scenario GetUserScenario(int scenarioId)
        {
            Scenario scenario = new Scenario();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_GetUserScenarios, conn);
                    cmd.Parameters.AddWithValue("@scenarioId", scenarioId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        scenario.Id = Convert.ToInt32(reader["scenario_id"]);
                        scenario.Name = Convert.ToString(reader["scenario_name"]);
                        scenario.Description = Convert.ToString(reader["description"]);
                        scenario.ImageName = Convert.ToString(reader["scenario_image"]);
                        scenario.Question = Convert.ToString(reader["question"]);
                        scenario.IsActive = Convert.ToBoolean(reader["isActive"]);

                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            scenario.AnswerList = GetScenarioAnswers(scenario.Id);

            return scenario;
        }

        public List<Scenario> GetAllScenarios()
        {
            List<Scenario> scenarios = new List<Scenario>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_GetAllScenarios, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Scenario scenario = new Scenario();

                        scenario.Id = Convert.ToInt32(reader["scenario_id"]);
                        scenario.Name = Convert.ToString(reader["scenario_name"]);
                        scenario.Description = Convert.ToString(reader["description"]);
                        scenario.ImageName = Convert.ToString(reader["scenario_image"]);
                        scenario.Question = Convert.ToString(reader["question"]);
                        //scenario.AnswerList = GetScenarioAnswers(scenario.Id);
                        scenario.IsActive = Convert.ToBoolean(reader["isActive"]);

                        scenarios.Add(scenario);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return scenarios;
        }

        public List<Answer> GetScenarioAnswers(int scenarioId)
        {
            List<Answer> answers = new List<Answer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_GetScenarioAnswers, conn);
                    cmd.Parameters.AddWithValue("@scenarioId", scenarioId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Answer answer = new Answer();

                        answer.AnswerId = Convert.ToInt32(reader["answer_id"]);
                        answer.AnswerText = Convert.ToString(reader["answer_text"]);
                        answer.ResponseImage = Convert.ToString(reader["response_image"]);
                        answer.ResponseText = Convert.ToString(reader["response_text"]);

                        answers.Add(answer);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return answers;
        }

        public Answer GetResponse(int answerId)
        {
            Answer answer = new Answer();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql_GetResponse, conn);
                cmd.Parameters.AddWithValue("@answerId", answerId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    answer.AnswerId = Convert.ToInt32(reader["answer_id"]);
                    answer.AnswerText = Convert.ToString(reader["answer_text"]);
                    answer.ResponseImage = Convert.ToString(reader["response_image"]);
                    answer.ResponseText = Convert.ToString(reader["response_text"]);
                    answer.ScenarioId = Convert.ToInt32(reader["scenario_id"]);
                }
            }
            return answer;
        }

        public Scenario GetNextScenario(int studentId, int scenarioId)
        {
            Scenario nextScenario = new Scenario();

            List<Scenario> userScenarios = GetAllUserScenarios(studentId);

            for (int i = 0; i < userScenarios.Count; i++)
            {
                if (userScenarios[i].Id == scenarioId)
                {
                    if ((i + 1) < userScenarios.Count)
                    {
                        nextScenario = userScenarios[i + 1];
                        break;

                    }
                    break;
                }
            }
            return nextScenario;
        }


        public Scenario ReplayScenario(int studentId, int scenarioId)
        {
            Scenario nextScenario = new Scenario();

            List<Scenario> userScenarios = GetAllUserScenarios(studentId);

  
           nextScenario = userScenarios[scenarioId - 1];
                   
  
            return nextScenario;
        }

        public List<Review> GetReview(int userId)
        {
            List<Review> reviewScenarios = new List<Review>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_GetReview, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Review review = new Review();

                        review.StudentId = Convert.ToInt32(reader["user_Id"]);
                        review.QuestionName = Convert.ToString(reader["scenario_name"]);
                        review.Description = Convert.ToString(reader["description"]);
                        review.Question = Convert.ToString(reader["question"]);
                        review.Answer = Convert.ToString(reader["answer_text"]);
                        review.Result = Convert.ToString(reader["response_text"]);
                        review.Date = Convert.ToDateTime(reader["date_answered"]);

                        reviewScenarios.Add(review);
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return reviewScenarios;

        }

        public bool SaveReview(int userId, int answerId)
        {
            bool isSaved = false;
            int rowAdded = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_SaveReview, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@answerId", answerId);

                    rowAdded = cmd.ExecuteNonQuery();

                    if (rowAdded > 0)
                    {
                        isSaved = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return isSaved;

        }

        public bool UpdateScenario(int id, bool isActive)
        {
            bool isSaved = false;
            int rowAdded = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_UpdateScenario, conn);
                    cmd.Parameters.AddWithValue("@isActive", isActive);
                    cmd.Parameters.AddWithValue("@scenarioId", id);
                    
                    rowAdded = cmd.ExecuteNonQuery();

                    if (rowAdded > 0)
                    {
                        isSaved = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return isSaved;

        }

        public bool CreateScenario(string name, string description, string imageName, string question, int isActive)
        {
            bool isSaved = false;
            isActive = 1;
            int rowAdded = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_CreateScenario, conn);
                    cmd.Parameters.AddWithValue("@scenarioName", name);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@image", imageName);
                    cmd.Parameters.AddWithValue("@question", question);
                    cmd.Parameters.AddWithValue("@isActive", isActive);

                    rowAdded = cmd.ExecuteNonQuery();

                    if (rowAdded > 0)
                    {
                        isSaved = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return isSaved;
        }

        public bool CreateAnswer(int id, string answerText, string responseText, string responseImage)
        {
            bool isSaved = false;
            int rowAdded = 0;
            id = GetMaxScenarioId();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_CreateAnswer, conn);
                    cmd.Parameters.AddWithValue("@scenarioId", id);
                    cmd.Parameters.AddWithValue("@answerText", answerText);
                    cmd.Parameters.AddWithValue("@responseText", responseText);
                    cmd.Parameters.AddWithValue("@responseImage", responseImage);

                    rowAdded = cmd.ExecuteNonQuery();

                    if (rowAdded > 0)
                    {
                        isSaved = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return isSaved;
        }

        public int GetMaxScenarioId()
        {
            int gotMax = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_GetMaxScenarioId, conn);

                    gotMax = (int)cmd.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return gotMax;
        }
    }
}
