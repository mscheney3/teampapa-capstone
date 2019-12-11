using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Account;

namespace WebApplication.Web.DAL.AssingmentDAL
{
    public interface IAssignmentDAL
    {
        IList<User> GetAllStudents();

        IList<User> GetAllTeachers();

        bool AssignStudent(int teacherID, int studentID);

        bool AssignScenario(int studentID, int scenarioID);




    }
}



