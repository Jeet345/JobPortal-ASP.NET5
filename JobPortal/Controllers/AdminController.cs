using JobPortal.Filters;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    [UserAuthenticateFilter]
    public class AdminController : Controller
    {
        private readonly JobPortalDBContext _context;

        public AdminController(JobPortalDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageUsers()
        {
            var users = _context.Users.Include(g => g.Group)
                .OrderByDescending(u => u.Id);
            return View(users);
        }

        public IActionResult ManageCategory()
        {
            var categories = _context.Categories
                .OrderByDescending(u => u.Id);
            return View(categories);
        }

        public IActionResult ManageEducation()
        {
            var educations = _context.JobSeekerEducations.Include(u => u.JobSeeker)
                .OrderByDescending(u => u.Id);
            return View(educations);
        }
        public IActionResult ManageExperience()
        {
            var experience = _context.JobSeekerExperiences.Include(u => u.JobSeeker)
                .OrderByDescending(u => u.Id);
            return View(experience);
        }
        public IActionResult ManageProject()
        {
            var projects = _context.JobSeekerProjects.Include(u => u.JobSeeker)
                .OrderByDescending(u => u.Id);
            return View(projects);
        }
        public IActionResult ManageJobs()
        {
            var jobs = _context.Jobs
                .Include(u => u.Employer)
                .Include(c => c.JobCategory)
                .OrderByDescending(u => u.Id);
            return View(jobs);
        }
        public IActionResult ManageJobApplications()
        {
            var jobApplications = _context.JobApplications.Include(c => c.Category)
                .Include(e => e.Employer)
                .Include(j => j.Job)
                .Include(u => u.JobSeeker)
                .OrderByDescending(u => u.Id);

            return View(jobApplications);
        }

    }
}
