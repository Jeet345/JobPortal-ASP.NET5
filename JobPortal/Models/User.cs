using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [MinLength(1, ErrorMessage = "Id Field Is Required")]
        public string Name { get; set; }

        [MinLength(1, ErrorMessage = "Email Field Is Required")]
        public string Email { get; set; }

        [MinLength(1, ErrorMessage = "Password Field Is Required")]
        public string Password { get; set; }

        [MinLength(1, ErrorMessage = "CompanyName Field Is Required")]
        public string CompanyName { get; set; }

        [MinLength(1, ErrorMessage = "Website Field Is Required")]
        public string Website { get; set; }

        [MinLength(1, ErrorMessage = "Address Field Is Required")]
        public string Address { get; set; }
        
        [MinLength(1, ErrorMessage = "AboutCompany Field Is Required")]
        public string AboutCompany { get; set; }

        [MinLength(1, ErrorMessage = "City cannot be less than 1")]
        public string City { get; set; }

        [MinLength(1, ErrorMessage = "State Field Is Required")]
        public string State { get; set; }

        [MinLength(1, ErrorMessage = "Country Field Is Required")]
        public string Country { get; set; }

        public DateTime? Dob { get; set; }

        [MinLength(1, ErrorMessage = "Gender Field Is Required")]
        public string Gender { get; set; }

        [MinLength(1, ErrorMessage = "Languages Field Is Required")]
        public string Languages { get; set; }

        [MinLength(1, ErrorMessage = "Resume Field Is Required")]
        public string Resume { get; set; }

        [MinLength(1, ErrorMessage = "WorkStatus Field Is Required")]
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
