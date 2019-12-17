using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Account;
using WebApplication.Web.Providers.Auth;
using WebApplication.Web.DAL;
using WebApplication.Web.DAL.AssingmentDAL;
using WebApplication.Web.DAL.ScenarioDAL;
using WebApplication.Web.Models.Scenario;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Web.Controllers
{    
    public class AccountController : Controller
    {
        private readonly IAuthProvider authProvider;
        private readonly IUserDAL userDAL;
        private readonly IAssignmentDAL assignmentDAL;
      

        private const string UserIdKey = "User_ID"; 

        public AccountController(IAuthProvider authProvider, IUserDAL userDAL, IAssignmentDAL assignmentDAL)
        {
            this.authProvider = authProvider;
            this.userDAL = userDAL;
            this.assignmentDAL = assignmentDAL;
        }
        
        //[AuthorizationFilter] // actions can be filtered to only those that are logged in
        [AuthorizationFilter("Admin", "Teacher", "Student")]  //<-- or filtered to only those that have a certain role
        [HttpGet]
        public IActionResult Index()
        {
            var user = authProvider.GetCurrentUser();
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            //Ensure the fields were filled out
            if (ModelState.IsValid)
            {
                //Check that they provided correct credentials
                bool validLogin = authProvider.SignIn(loginViewModel.Email, loginViewModel.Password);
                if (validLogin)
                {


                        User currentUser = authProvider.GetCurrentUser();
                        string currentRole = currentUser.Role;
                        HttpContext.Session.SetString("Role", currentRole);

                    //Redirect the user where you want them to go after successful login
                    return RedirectToAction("Index", "Scenario");
                }
            }

            return View(loginViewModel);
        }
        
        [HttpGet]
        public IActionResult LogOff()
        {
            // Clear user from session
            authProvider.LogOff();

            // Redirect the user where you want them to go after logoff
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [AuthorizationFilter("Admin")]  //<-- or filtered to only those that have a certain role
        [HttpGet]
        public IActionResult ListOfUsers()
        {
            List<User> allUsers = userDAL.GetAllUsers();

            User user = new User();

            TempData["User"] = user;

            return View(allUsers);
        }

        [HttpPost]
        public IActionResult ResetUser(User user)
        {

            User loggedIn = authProvider.GetCurrentUser();

            if (loggedIn == null)
            {
                return RedirectToAction("Error", "Account");
            }

            HashProvider hashProvider = new HashProvider();

            HashedPassword newPasscode = hashProvider.HashPassword(user.Password);

            user.Salt = newPasscode.Salt;
            user.Password = newPasscode.Password;
            userDAL.UpdateUser(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AuthorizationFilter("Admin", "Teacher")]
        public IActionResult CreateUser(User user)
        {

            User loggedIn = authProvider.GetCurrentUser();

            if (loggedIn == null)
            {
                return RedirectToAction("Login", "Account");
            }

            User currentUser = authProvider.GetCurrentUser();
            TempData["User"] = currentUser;


            return View();
        }

        [AuthorizationFilter("Admin", "Teacher")]
        public IActionResult CreateUserForm(User user)
        {

            HashProvider hashProvider = new HashProvider();
            HashedPassword newPasscode = hashProvider.HashPassword(user.Password);

            user.Salt = newPasscode.Salt;
            user.Password = newPasscode.Password;

            userDAL.CreateUser(user);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AssignStudent(User user)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {

            User loggedIn = authProvider.GetCurrentUser();

            if (loggedIn == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            // Ensure the fields were filled out
            if (ModelState.IsValid)
            {
                // Check that they provided correct credentials
                bool validLogin = authProvider.ChangePassword(changePasswordModel.OldPassword, changePasswordModel.NewPassword );


                if (validLogin)
                {
                    // Redirect the user where you want them to go after successful login
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(changePasswordModel);
        }



        [HttpGet]
        public IActionResult AssignStudent()
        {

            User loggedIn = authProvider.GetCurrentUser();

            if (loggedIn == null)
            {
                return RedirectToAction("Login", "Account");
            }
            User currentUser = authProvider.GetCurrentUser();

            IList<User> students = assignmentDAL.GetAllStudents();
            TempData["students"] = students;


            IList<User> teachers = assignmentDAL.GetAllTeachers();
            TempData["teachers"] = teachers;


            return View();
        }

        [HttpGet]
        public IActionResult AssignStudentToTeacher(int studentId, int teacherId)
        {


            assignmentDAL.AssignStudent(studentId, teacherId);

            return RedirectToAction("AssignStudent", "Account");
        }








        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                // Register them as a new user (and set default role)
                // When a user registers they need to be given a role. If you don't need anything special
                // just give them "User".

                authProvider.Register(registerViewModel.Email, registerViewModel.Password, role: "Student");



                // Redirect the user where you want them to go after registering
                return RedirectToAction("Index", "Home");
            }

            return View(registerViewModel);
        }
    }
}