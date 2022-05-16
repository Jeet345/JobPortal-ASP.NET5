using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortal.Models
{
    public partial class JobSeekerExperience
    {
        public int Id { get; set; }
        public int? JobSeekerId { get; set; }
        public string Experience { get; set; }
        public string Designation { get; set; }
        public string Organization { get; set; }
        public int? StartMonth { get; set; }
        public int? EndMonth { get; set; }
        public int? Salary { get; set; }
        public string JobProfile { get; set; }

        public virtual User JobSeeker { get; set; }
    }
}
