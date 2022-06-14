using JobPortal.Filters;
using JobPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var jobs = _context.Jobs
                .Include(j => j.JobCategory)
                .OrderByDescending(j => j.Id)
                .Take(7);

            ViewData["Category"] = _context.Categories.ToList();

            return View(jobs);
        }
        public IActionResult AllJobs()
        {
            var jobs = _context.Jobs
                .Where(j => j.JobStatus == 1)
                .OrderByDescending(j => j.Id);

            return View(jobs);
        }


        [HttpGet]
        public IActionResult SearchJobs(string title, string location, string category)
        {

            if (!String.IsNullOrEmpty(title))
            {

                if (!String.IsNullOrEmpty(location) && !String.IsNullOrEmpty(category))
                {
                    var jobs = _context.Jobs
                        .Include(j => j.JobCategory)
                        .Where(j => j.JobLocation.Contains(location)
                            && j.JobTitle.Contains(title)
                            && j.JobCategory.CategoryName.Contains(category))
                        .Where(j => j.JobStatus == 1)
                        .OrderByDescending(j => j.Id);

                    return View(jobs);

                }
                else if (!String.IsNullOrEmpty(location) && String.IsNullOrEmpty(category))
                {
                    var jobs = _context.Jobs
                        .Include(j => j.JobCategory)
                        .Where(j => j.JobLocation.Contains(location)
                            && j.JobTitle.Contains(title))
                        .Where(j => j.JobStatus == 1)
                        .OrderByDescending(j => j.Id);

                    return View(jobs);

                }
                else if (!String.IsNullOrEmpty(category) && String.IsNullOrEmpty(location))
                {
                    var jobs = _context.Jobs
                        .Include(j => j.JobCategory)
                        .Where(j => j.JobTitle.Contains(title)
                            && j.JobCategory.CategoryName.Contains(category))
                        .Where(j => j.JobStatus == 1)
                        .OrderByDescending(j => j.Id);

                    return View(jobs);
                }
                else
                {
                    var jobs = _context.Jobs
                        .Include(j => j.JobCategory)
                        .Where(j => j.JobTitle.Contains(title))
                        .Where(j => j.JobStatus == 1)
                        .OrderByDescending(j => j.Id);

                    return View(jobs);
                }
            }
            return NotFound();
        }

        [UserAuthenticateFilter]
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
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
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
            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            var jobs = (from j in _context.Jobs
                        join c in _context.Categories on j.JobCategoryId equals c.Id
                        where j.EmployerId == userId
                        select new Job {
                             EmployerId = j.EmployerId,
                             Id = j.Id,
                             JobApplications = j.JobApplications,
                             JobCategory = c,
                             JobLocation = j.JobLocation,
                             JobPostingDate = j.JobPostingDate,
                             JobSalary = j.JobSalary,
                             JobTitle = j.JobTitle,
                             JobType = j.JobType,
                             JobStatus = j.JobStatus
                        }).OrderByDescending(j => j.Id);

            return View(jobs);
        }

        [HttpPost]
        public IActionResult JobApplications(string jobId)
        {
            int id = int.Parse(jobId);

            var jobApplications = _context.JobApplications
                .Include(u => u.JobSeeker)
                .Include(e => e.Employer)
                .Include(j => j.Job)
                .Where(j => j.JobId == id)
                .OrderByDescending(a => a.Id);

            return View(jobApplications);
        }

        [HttpPost]
        public bool HireUserRequest(string appId)
        {
            if(!String.IsNullOrEmpty(appId))
            {
                int id = int.Parse(appId);

                var jobApplication = _context.JobApplications
                    .Include(u => u.JobSeeker)
                    .FirstOrDefault(a => a.Id == id);

                if(jobApplication == null)
                {
                    return false;
                }

                jobApplication.Status = 2; // 2 means user is hired

                _context.Update(jobApplication);
                _context.SaveChanges();

                var hireMail = new SendMail
                {
                    toMail = jobApplication.JobSeeker.Email.ToString(),
                    mailBody = "<h2>Your Application Selected For Hiring.</h2>",
                    mailTitle = "Clear Career Job Portal",
                    subject = "Applied Job"
                };
                hireMail.Send();

                return true;
            }
            return false;
        }

        [HttpPost]
        public bool RejectUserRequest(string appId)
        {
            if (!String.IsNullOrEmpty(appId))
            {
                int id = int.Parse(appId);

                var jobApplication = _context.JobApplications
                                    .Include(u => u.JobSeeker)
                                    .FirstOrDefault(a => a.Id == id);

                if (jobApplication == null)
                {
                    return false;
                }

                jobApplication.Status = 0; // 0 means user is reject

                _context.Update(jobApplication);
                _context.SaveChanges();

                var rejectMail = new SendMail
                {
                    toMail = jobApplication.JobSeeker.Email.ToString(),
                    mailBody = "<h2 style='color:red'>Sorry, Your Application Is Rejected.</h2>",
                    mailTitle = "Clear Career Job Portal",
                    subject = "Applied Job"
                };
                rejectMail.Send();

                return true;
            }
            return false;
        }


        [HttpDelete]
        public bool DeleteJobRequest(int? id)
        {

            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            if (id == null)
            {
                return false;
            }

            var job = _context.Jobs.Find(id);

            if (job == null || job.EmployerId != userId)
            {
                return false;
            }

            job.JobStatus = 0;

            _context.Update(job);
            _context.SaveChanges();

            TempData["success"] = "Job Status Changed !!";

            return true;
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
                string date = DateTime.Now.ToString("dd/MM/yyyy");
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
