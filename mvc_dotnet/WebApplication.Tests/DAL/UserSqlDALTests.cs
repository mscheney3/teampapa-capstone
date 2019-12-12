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
using WebApplication.Web.Models;

namespace WebApplication.Tests.DAL
{
    [TestClass]
    public class UserSqlDALTests
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
        public void Assignment_CreateUser_Test()
        {
            //Arrange
            int initialRowCount = GetRowCount("users");

            UserSqlDAL dao = new UserSqlDAL(ConnectionString);

            User inputUser = new User();
            inputUser.Password = "UT3zZWao733yZCgthmNJJxs5NCg=";
            inputUser.Salt = "NuE0Y6FonAI=";
            inputUser.Role = "Teacher";
            inputUser.Username = "test@test.com";

            dao.CreateUser(inputUser);

            //Action
            int results = GetRowCount("users");
            //Assert
            Assert.AreEqual(initialRowCount+1, results);
        }


        [TestMethod]
        public void Assignment_GetUser_Test()
        {
            //Arrange
            UserSqlDAL dao = new UserSqlDAL(ConnectionString);

            User inputUser = new User();
            inputUser.Password = "UT3zZWao733yZCgthmNJJxs5NCg=";
            inputUser.Salt = "NuE0Y6FonAI=";
            inputUser.Role = "Teacher";
            inputUser.Username = "test@test.com";

            dao.CreateUser(inputUser);

            //Action
            User testUser = dao.GetUser(inputUser.Username);
            ////Assert
            Assert.AreEqual(inputUser.Username, testUser.Username);
        }

        [TestMethod]
        public void Assignment_UpdateUser_Test()
        {
            //Arrange
            UserSqlDAL dao = new UserSqlDAL(ConnectionString);

            User inputUser = new User();
            inputUser.Password = "UT3zZWao733yZCgthmNJJxs5NCg=";
            inputUser.Salt = "NuE0Y6FonAI=";
            inputUser.Role = "Teacher";
            inputUser.Username = "test@test.com";


            dao.CreateUser(inputUser);

            inputUser = dao.GetUser(inputUser.Username);

            inputUser.Role = "Admin";
            dao.UpdateUser(inputUser);

            //Action
            User testUser = dao.GetUser(inputUser.Username);
            //Assert
            Assert.AreEqual(testUser.Role, inputUser.Role);
        }


        [TestMethod]
        public void Assignment_DeleteUser_Test()
        {
            //Arrange
            UserSqlDAL dao = new UserSqlDAL(ConnectionString);

            User inputUser = new User();
            inputUser.Password = "UT3zZWao733yZCgthmNJJxs5NCg=";
            inputUser.Salt = "NuE0Y6FonAI=";
            inputUser.Role = "Teacher";
            inputUser.Username = "test@test.com";


            dao.CreateUser(inputUser);

            int test = GetRowCount("users");
            inputUser = dao.GetUser(inputUser.Username);
            //Action
            dao.DeleteUser(inputUser);
            int result = GetRowCount("users");
            //Assert
            Assert.AreEqual(result, test-1);
        }


        [TestMethod]
        public void Assignment_GetAllUsers_Test()
        {
            //Arrange
            IUserDAL dao = new UserSqlDAL(ConnectionString);
            IList<User> testList = new List<User>();
            testList = dao.GetAllUsers();
            //Action
            int results = GetRowCount("users");
            //Assert
            Assert.AreEqual(results, testList.Count);
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
