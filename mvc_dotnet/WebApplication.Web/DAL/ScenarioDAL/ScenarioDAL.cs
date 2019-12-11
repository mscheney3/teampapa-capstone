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
        private readonly string sql_saveReview = "INSERT INTO review (user_id, answer_id) VALUES (@userId, @answerId)";

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
                        scenario.AnswerList = GetScenarioAnswers(scenario.Id);

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
                        scenario.AnswerList = GetScenarioAnswers(scenario.Id);

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
            //Scenario currentScenario = GetUserScenario(scenarioId);

            List<Scenario> userScenarios = GetAllUserScenarios(studentId);

            for (int i = 0; i < userScenarios.Count; i++)
            {
                if (userScenarios[i].Id == scenarioId)
                {
                    if ((i+1)<userScenarios.Count)
                    {
                        nextScenario = userScenarios[i + 1];
                        break;

                    }
                    break;
                }
            }

            return nextScenario;

            //int currentIndex = userScenarios.IndexOf(currentScenario);
            //currently fails because Answer list is null in the list
            //if (userScenarios.Count > (currentIndex + 1))
                //{
                //    nextScenario = userScenarios[currentIndex + 1];
                //}


        }
        public Review saveReview()
        {
            Review review = new Review();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_saveReview, conn);
                    cmd.Parameters.AddWithValue("@userId", review.userId);
                    cmd.Parameters.AddWithValue("@answerId", review.answerId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return review;
        }

    }
}
