using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL.ScenarioDAL;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Scenario;
using WebApplication.Web.Providers.Auth;

namespace WebApplication.Web.Controllers
{
    public class ScenarioController : Controller
    {
        private IScenarioDAL scenarioDAL;
        private readonly IAuthProvider authProvider;


        public ScenarioController(IScenarioDAL scenarioDAL, IAuthProvider authProvider)
        {
            this.scenarioDAL = scenarioDAL;
            this.authProvider = authProvider;
        }

        public IActionResult Index()
        {
            User user = authProvider.GetCurrentUser();
            List<Scenario> scenarios = scenarioDAL.GetAllUserScenarios(user.Id);

            return View(scenarios);
        }

        public IActionResult Scenario(int id)
        {
            Scenario scenario = scenarioDAL.GetUserScenario(id);
            return View(scenario);
        }

        public IActionResult Response(int id)
        {
            Answer response = scenarioDAL.GetResponse(id);
            return View(response);
        }
    }
}