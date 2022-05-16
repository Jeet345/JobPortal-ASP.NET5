using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortal.Models
{
    public partial class Category
    {
        public Category()
        {
            JobApplications = new HashSet<JobApplication>();
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<JobApplication> JobApplications { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
