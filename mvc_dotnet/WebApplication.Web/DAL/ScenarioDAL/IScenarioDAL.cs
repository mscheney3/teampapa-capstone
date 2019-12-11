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

        Review saveReview(); 

    }
}
