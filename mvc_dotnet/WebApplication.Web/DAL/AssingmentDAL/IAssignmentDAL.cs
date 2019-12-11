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

        bool AssignStudent(User teacherID, User studentID);




    }
}



