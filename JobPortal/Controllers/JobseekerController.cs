using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    public class JobseekerController : Controller
    {

        private readonly JobPortalDBContext _context;

        public JobseekerController(JobPortalDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        public IActionResult AllJobs()
        {
            return View();
        }
        public IActionResult Account()
        {
            return View();
        }
        public IActionResult AppliedJobs()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public IActionResult JobDetail()
        {
            return View();
        }

    }
}
