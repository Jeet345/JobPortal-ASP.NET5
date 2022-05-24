using JobPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly JobPortalDBContext _context;

        public AccountController(JobPortalDBContext context)
        {
            _context = context;
        }

        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public Boolean LoginRequest(MyLogin myLogin)
        {
            if (ModelState.IsValid)
            {
                var query = _context.Users
                    .Where(u => u.Email == myLogin.email)
                    .Select(u => u.Password)
                    .FirstOrDefault();

                if (!String.IsNullOrEmpty(query))
                {
                    bool passwordCheck = BCrypt.Net.BCrypt.Verify(myLogin.password, query.ToString());

                    if (passwordCheck)
                    {
                        TempData["loginSuccess"] = "Login Successfully..";
                        HttpContext.Session.SetString("userEmail", myLogin.email);
                        return true;
                    }
                }
            }
            return false;
        }
        

        [Route("/Signup")]
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public Boolean SignupRequest(MySignup mySignup)
        {
            if (ModelState.IsValid)
            {
                if (isEmailExist(mySignup.email))
                {
                    TempData["SignupFailed"] = "Email Is Already Exist !!";
                    return false;
                }
                else
                {
                    User user = new User()
                    {
                        Email = mySignup.email,
                        Password = BCrypt.Net.BCrypt.HashPassword(mySignup.password),
                        Name = mySignup.username,
                        GroupId = int.Parse(mySignup.groupId)
                    };

                    _context.Add(user);
                    _context.SaveChanges();

                    TempData["SignupDone"] = "SignUp Successfully, Please Login !!";

                    return true;
                }
            }
            else
            {
                TempData["SignupFailed"] = "Something Wan't Wrong !!";
                return false;
            }
        }

        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userEmail");
            return Redirect("/Login");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public Boolean isEmailExist(string email)
        {
            if (email != "")
            {
                var query = from u in _context.Users
                            where u.Email == email
                            select u;

                if(query.Count() > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
