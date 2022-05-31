using JobPortal.Filters;
using JobPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
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

        public IActionResult AllJobs()
        {
            var jobs = _context.Jobs.ToList();

            return View(jobs);
        }

        [UserAuthenticateFilter]
        [HttpGet]
        public async Task<IActionResult> Account()
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            var jobseeker = await _context.Users.FindAsync(userId);

            var jobseekerEducation = (from e in _context.JobSeekerEducations
                                            where e.JobSeekerId == jobseeker.Id
                                            select e)
                                            .ToList();

            var jobseekerProject = (from p in _context.JobSeekerProjects
                                      where p.JobSeekerId == jobseeker.Id
                                      select p)
                                     .ToList();

            var jobseekerExperience = (from e in _context.JobSeekerExperiences
                                    where e.JobSeekerId == jobseeker.Id
                                    select e)
                                     .ToList();

            var jobseekerSkills = (from s in _context.JobSeekerSkills
                                       where s.JobSeekerId == jobseeker.Id
                                       select s)
                                     .ToList();

            jobseeker.JobSeekerSkills = jobseekerSkills;
            jobseeker.JobSeekerExperiences = jobseekerExperience;
            jobseeker.JobSeekerProjects = jobseekerProject;
            jobseeker.JobSeekerEducations = jobseekerEducation;

            if (jobseeker == null)
            {
                return NotFound();
            }
        
            return View(jobseeker);
        }

        [HttpPost]
        public bool AccountRequest(User userToUpdate)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    User user = _context.Users.Find(userToUpdate.Id);

                    user.Name = userToUpdate.Name;
                    user.Gender = userToUpdate.Gender;
                    user.Address = userToUpdate.Address;
                    user.Dob = userToUpdate.Dob;
                    user.City = userToUpdate.City;
                    user.Country = userToUpdate.Country;
                    user.Languages = userToUpdate.Languages;
                    user.State = userToUpdate.State;
                    user.WorkStatus = userToUpdate.WorkStatus;

                    _context.Update(user);
                    _context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException )
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

        [UserAuthenticateFilter]
        public IActionResult AppliedJobs()
        {
            return View();
        }

        [UserAuthenticateFilter]
        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult JobDetail(int? id)
        {
            if(id == null)
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

    }
}
