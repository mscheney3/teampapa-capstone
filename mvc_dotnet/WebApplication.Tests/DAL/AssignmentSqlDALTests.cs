using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using WebApplication.Web.DAL.AssingmentDAL;
using WebApplication.Web.Models;
using System.Data.SqlClient;

namespace WebApplication.Tests.DAL
{
    [TestClass]
    public class AssignmentSqlDALTests
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
            public void Assignment_GetAllStudents_Test()
            {
                //Arrange
                IAssignmentDAL dao = new AssignmentDAL(ConnectionString);
                IList<User> testList = new List<User>();
                testList = dao.GetAllStudents();
                //Action
                int results = GetRowCount("users", "Student");
                //Assert
                Assert.AreEqual(results, testList.Count);
            }


        [TestMethod]
        public void Assignment_GetAllTeachers_Test()
        {
            //Arrange
            IAssignmentDAL dao = new AssignmentDAL(ConnectionString);
            IList<User> testList = new List<User>();
            testList = dao.GetAllStudents();
            //Action
            int results = GetRowCount("users", "Teacher");
            //Assert
            Assert.AreEqual(results, testList.Count);
        }

        [TestMethod]
        public void Assignment_AssignStudent_Test()
        {
            //Arrange
            IAssignmentDAL dao = new AssignmentDAL(ConnectionString);

            //Action
            bool test = dao.AssignStudent(1, 3);
            //Assert
            Assert.IsTrue(test);
        }


        [TestMethod]
        public void Assignment_AssignScenario_Test()
        {
            //Arrange
            IAssignmentDAL dao = new AssignmentDAL(ConnectionString);

            //Action
            bool test = dao.AssignScenario(1, 3);
            //Assert
            Assert.IsTrue(test);
        }


        [TestMethod]
        public void Assignment_UnAssignScenario_Test()
        {
            //Arrange
            IAssignmentDAL dao = new AssignmentDAL(ConnectionString);

            //Action
            dao.AssignScenario(1, 3);
            bool test = dao.UnassignScenario(1, 3);
            //Assert
            Assert.IsTrue(test);
        }

        /// <summary>
        /// Gets the row count for a table.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected int GetRowCount(string table, string role)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table} WHERE role = '{role}'", conn);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count;
                }
            }





        }
    }
