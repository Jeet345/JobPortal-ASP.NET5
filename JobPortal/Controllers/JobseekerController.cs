using JobPortal.Filters;
using JobPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            var jobs = _context.Jobs
                .Include(j => j.JobCategory)
                .OrderByDescending(j => j.Id)
                .Take(7);

            ViewData["Category"] = _context.Categories.ToList();

            return View(jobs);
        }

        public IActionResult AllJobs()
        {
            //var sendMail = new SendMail
            //{
            //    toMail = "mendaparajeet@gmail.com",
            //    mailBody = "<h2>Your Application Selected For Hiring.</h2>",
            //    mailTitle = "Clear Career Job Portal",
            //    subject = "Applied Job"
            //};
            //sendMail.Send();

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
        public bool AddEducationRequest(JobSeekerEducation newEducation)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(HttpContext.Session.GetString("userId"));

                User user = _context.Users.Find(userId);
                
                if (user == null)
                {
                    return false;
                }

                JobSeekerEducation educationToAdd = new JobSeekerEducation
                {
                    Education = newEducation.Education,
                    Course = newEducation.Course,
                    JobSeekerId = userId,
                    Marks = newEducation.Marks,
                    PassingYear = newEducation.PassingYear,
                    University = newEducation.University
                };

                _context.Add(educationToAdd);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        [HttpPost]
        public IActionResult AddExperienceRequest(JobSeekerExperience newExperience)
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            User user = _context.Users.Find(userId);

            if (user == null)
            {
                return NotFound();
            }

            JobSeekerExperience experienceToAdd = new JobSeekerExperience
            {
                Designation = newExperience.Designation,
                EndMonth = newExperience.EndMonth,
                StartMonth = newExperience.StartMonth,
                Experience = newExperience.Experience,
                JobSeekerId = userId,
                Organization = newExperience.Organization,
                Salary = newExperience.Salary,
                JobProfile = newExperience.JobProfile
            };

            _context.Add(experienceToAdd);
            _context.SaveChanges();

            return Redirect("/Jobseeker/Account");
        }
        
        [HttpPost]
        public IActionResult AddProjectRequest(JobSeekerProject newProject)
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            User user = _context.Users.Find(userId);

            if (user == null)
            {
                return NotFound();
            }

            JobSeekerProject projectToAdd = new JobSeekerProject
            {
                JobSeekerId = userId,
                ProjectClient = newProject.ProjectClient,
                ProjectDetails = newProject.ProjectDetails,
                ProjectSkilles = newProject.ProjectSkilles,
                ProjectStatus = newProject.ProjectStatus,
                ProjectTitle = newProject.ProjectTitle,
                StartMonth = newProject.StartMonth,
                EndMonth = newProject.EndMonth
            };

            _context.Add(projectToAdd);
            _context.SaveChanges();

            return Redirect("/Jobseeker/Account");
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
                catch(System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
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

            int userId = int.Parse(HttpContext.Session.GetString("userId"));

            var jobs = _context.JobApplications
                .Include(j => j.Job)
                .Include(c => c.Category)
                .Include(e => e.Employer)
                .Include(e => e.JobSeeker)
                .Where(j => j.JobSeekerId == userId)
                .OrderByDescending(j => j.Id);

            return View(jobs);
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

        [HttpPost]
        public int JobApplyRequest(string jobId)
        {
            if (!String.IsNullOrEmpty(jobId))
            {
                int id = int.Parse(jobId);
                int userId = int.Parse(HttpContext.Session.GetString("userId"));
                string date = DateTime.Now.ToString("dd/MM/yyyy");

                var isAlredyApplied = from a in _context.JobApplications
                                      where a.JobSeekerId == userId
                                      && a.JobId == id
                                      select a;

                if (isAlredyApplied.Count() > 0)
                {
                    return 2;
                }
                else
                {
                    var job = _context.Jobs.Find(id);
                    job.JobApplications = job.JobApplications + 1;

                    JobApplication jobApplication = new JobApplication
                    {
                        ApplicationDate = date,
                        CategoryId = job.JobCategoryId,
                        EmployerId = job.EmployerId,
                        JobId = job.Id,
                        JobSeekerId = userId
                    };

                    _context.Update(job);
                    _context.Add(jobApplication);
                    _context.SaveChanges();

                    return 1;
                }
            }
            return 0;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

    }
}
