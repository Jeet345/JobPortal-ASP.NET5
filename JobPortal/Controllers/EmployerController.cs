using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    public class EmployerController : Controller
    {

        private readonly JobPortalDBContext _context;

        public EmployerController(JobPortalDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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
        public IActionResult ManageJobs()
        {
            return View();
        }
        public IActionResult JobDetail()
        {
            return View();
        }
        public IActionResult PostJob()
        {
            return View();
        }
    }
}
