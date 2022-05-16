using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortal.Models
{
    public partial class JobSeekerProject
    {
        public int Id { get; set; }
        public int? JobSeekerId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectClient { get; set; }
        public string ProjectStatus { get; set; }
        public int? StartMonth { get; set; }
        public int? EndMonth { get; set; }
        public string ProjectDetails { get; set; }
        public string ProjectSkilles { get; set; }

        public virtual User JobSeeker { get; set; }
    }
}
