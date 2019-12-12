using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Web.DAL;
using WebApplication.Web.DAL.AssingmentDAL;
using WebApplication.Web.DAL.ScenarioDAL;
using WebApplication.Web.Models.Scenario;

namespace WebApplication.Tests.DAL
{
    [TestClass]
    public class ScenarioSqlDALTests
    {
        string ConnectionString { get; } = "Data Source=.\\SQLEXPRESS;Initial Catalog=GamePrototype;Integrated Security=True";
        private TransactionScope transaction;


        [TestInitialize]
        public void Setup()
        {
            // Begin the transaction
            transaction = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        [TestMethod]
        public void Scenario_GetReview_Test()
        {
            //Arrange
            IScenarioDAL dao = new ScenarioDAL(ConnectionString);

             bool nextScenario = dao.SaveReview(1, 1);


            //Action
            List<Review> testList = new List<Review>();
            testList = dao.GetReview(1);

            //Assert
            Assert.AreEqual(1, testList.Count);
        }


        [TestMethod]
        public void Scenario_GetNextScenario_Test()
        {
            //Arrange
            IAssignmentDAL assignmentDAO = new AssignmentDAL(ConnectionString);
            assignmentDAO.AssignScenario(1, 1);

            assignmentDAO.AssignScenario(1, 3);


            IScenarioDAL dao = new ScenarioDAL(ConnectionString);

            Scenario nextScenario = dao.GetNextScenario(1, 1);
  

            //Action
            int id = 3;
            //Assert
            Assert.AreEqual(id, nextScenario.Id);
        }


        [TestMethod]
        public void Scenario_SaveReview_Test()
        {
            //Arrange
            IScenarioDAL dao = new ScenarioDAL(ConnectionString);
            bool isAdded = false;


            //Action
            isAdded = dao.SaveReview(1, 1);
            //Assert
            Assert.IsTrue(isAdded);
        }

        //[TestMethod]
        //public void Scenario_GetResponse_Test()
        //{
        //    {
        //    //    //Wait until after optional create scenario is complete
        //    }
        //}


        //[TestMethod]
        //public void Scenario_GetScenarioAnswers_Test()
        //{
        //    {
        //    //    //Wait until after optional create scenario is complete
        //    }
        //}

        //[TestMethod]
        //public void Scenario_GetUserScenario_Test()
        //{
        //    //Wait until after optional create scenario is complete
        //}

        //[TestMethod]
        //public void Scenario_UpdateScenario_Test()
        //{
        //    //Wait until after optional create scenario is complete
        //}

        [TestMethod]
        public void Scenario_GetAllUserScenarios_Test()
        {
            //Arrange
            IAssignmentDAL assignmentDAO = new AssignmentDAL(ConnectionString);
            assignmentDAO.AssignScenario(1, 1);

            IScenarioDAL dao = new ScenarioDAL(ConnectionString);
            IList<Scenario> testList = new List<Scenario>();
            testList = dao.GetAllUserScenarios(1);

            //Action
            int result = 1;
            //Assert
            Assert.AreEqual(result, testList.Count);
        }




        [TestMethod]
        public void Scenario_GetAllScenarios_Test()
        {
            //Arrange
            IScenarioDAL dao = new ScenarioDAL(ConnectionString);
            IList<Scenario> testList = new List<Scenario>();
            testList = dao.GetAllScenarios();
            //Action
            int results = GetRowCount("scenarios");
            //Assert
            Assert.AreEqual(results, testList.Count);
        }

        /// <summary>
        /// Gets the row count for a table.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected int GetRowCount(string table, int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table} WHERE scenario_id = '{id}'", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }

        protected int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
    }
}
