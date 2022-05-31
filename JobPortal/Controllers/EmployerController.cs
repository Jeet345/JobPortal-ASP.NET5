using JobPortal.Filters;
using JobPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    [UserAuthenticateFilter]
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
            var jobs = _context.Jobs.ToList();

            return View(jobs);
        }
        public IActionResult Account()
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            var user = _context.Users.Find(userId);

            if(user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public bool AccountRequest(User userToUpdate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = _context.Users.Find(userToUpdate.Id);

                    user.CompanyName = userToUpdate.CompanyName;
                    user.City = userToUpdate.City;
                    user.Address = userToUpdate.Address;
                    user.AboutCompany = userToUpdate.AboutCompany;
                    user.Country = userToUpdate.Country;
                    user.State = userToUpdate.State;
                    user.Website = userToUpdate.Website;

                    _context.Update(user);
                    _context.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userToUpdate.Id))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
                return true;    
            }
            return false;
        }

        public IActionResult ManageJobs()
        {
            return View();
        }
        public IActionResult JobDetail(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var job = _context.Jobs.Find(id);

            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }
        public IActionResult PostJob()
        {
            ViewData["Category"] = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public bool PostJobRequest(Job newJob)
        {
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                int empId = int.Parse(HttpContext.Session.GetString("userId"));

                newJob.JobPostingDate = date;
                newJob.EmployerId = empId;

                _context.Add(newJob);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
