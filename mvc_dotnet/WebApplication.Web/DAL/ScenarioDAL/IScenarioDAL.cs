using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models.Scenario;

namespace WebApplication.Web.DAL.ScenarioDAL
{
    public interface IScenarioDAL
    {
        List<Scenario> GetAllUserScenarios(int studentId);

        Scenario GetUserScenario(int scenarioId);

        List<Scenario> GetAllScenarios();

        List<Answer> GetScenarioAnswers(int scenarioId);

        Answer GetResponse(int answerId);

        Scenario GetNextScenario(int studentId, int scenarioId);

        List<Review> GetReview(int userId);

        bool SaveReview(int userId, int answerId);

        bool UpdateScenario(int id, string name, string description, string image, string question, bool isActive);

        bool CreateScenario(string scenarioName, string description, string image, string question, int isActive);

        bool CreateAnswer(int id, string answerText, string responseText, string responseImage, string color, string emoji);

        int GetMaxScenarioId();

        Scenario ReplayScenario(int studentId, int scenarioId);


    }
}
