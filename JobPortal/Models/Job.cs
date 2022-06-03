using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace JobPortal.Models
{
    public partial class Job
    {
        public Job()
        {
            JobApplicationsNavigation = new HashSet<JobApplication>();
        }

        public int Id { get; set; }
        public int? EmployerId { get; set; }

        public int? JobCategoryId { get; set; }

        [MinLength(1, ErrorMessage = "Job Title Field Is Required")]
        public string JobTitle { get; set; }

        [MinLength(1, ErrorMessage = "Job Location Field Is Required")]
        public string JobLocation { get; set; }

        [MinLength(1, ErrorMessage = "Job Experience Field Is Required")]
        public string JobExperience { get; set; }

        public int? JobApplications { get; set; }

        public string JobPostingDate { get; set; }

        [MinLength(1, ErrorMessage = "Job Skills Field Is Required")]
        public string JobSkills { get; set; }

        [Range(1000, 100000000, ErrorMessage = "Please Enter Valid Salary")]
        public int? JobSalary { get; set; }

        [MinLength(1, ErrorMessage = "Job Type Field Is Required")]
        public string JobType { get; set; }

        [MinLength(1, ErrorMessage = "Job Education Field Is Required")]
        public string JobEducation { get; set; }

        public int? JobStatus { get; set; }

        public virtual User Employer { get; set; }
        public virtual Category JobCategory { get; set; }

        public virtual ICollection<JobApplication> JobApplicationsNavigation { get; set; }
    }
}
