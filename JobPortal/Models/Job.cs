using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortal.Models
{
    public partial class Job
    {
        public int Id { get; set; }
        public int? EmployerId { get; set; }
        public int? JobCategoryId { get; set; }
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }
        public string JobExperience { get; set; }
        public int? JobApplications { get; set; }
        public DateTime? JobPostingDate { get; set; }
        public string JobSkills { get; set; }
        public int? JobSalary { get; set; }
        public string JobType { get; set; }
        public string JobEducation { get; set; }
        public int? JobStatus { get; set; }

        public virtual User Employer { get; set; }
        public virtual Category JobCategory { get; set; }
    }
}
