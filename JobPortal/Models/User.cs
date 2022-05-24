using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortal.Models
{
    public partial class User
    {
        public User()
        {
            JobApplicationEmployers = new HashSet<JobApplication>();
            JobApplicationJobSeekers = new HashSet<JobApplication>();
            JobSeekerEducations = new HashSet<JobSeekerEducation>();
            JobSeekerExperiences = new HashSet<JobSeekerExperience>();
            JobSeekerProjects = new HashSet<JobSeekerProject>();
            JobSeekerSkills = new HashSet<JobSeekerSkill>();
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        public int? GroupId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string AboutCompany { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string Languages { get; set; }
        public string Resume { get; set; }
        public string WorkStatus { get; set; }
        public int? Status { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<JobApplication> JobApplicationEmployers { get; set; }
        public virtual ICollection<JobApplication> JobApplicationJobSeekers { get; set; }
        public virtual ICollection<JobSeekerEducation> JobSeekerEducations { get; set; }
        public virtual ICollection<JobSeekerExperience> JobSeekerExperiences { get; set; }
        public virtual ICollection<JobSeekerProject> JobSeekerProjects { get; set; }
        public virtual ICollection<JobSeekerSkill> JobSeekerSkills { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }


    }
}
