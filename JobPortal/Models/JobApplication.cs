using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortal.Models
{
    public partial class JobApplication
    {
        public int Id { get; set; }
        public int? JobSeekerId { get; set; }
        public int? EmployerId { get; set; }
        public int? JobCategoryId { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public int? Status { get; set; }

        public virtual User Employer { get; set; }
        public virtual Category JobCategory { get; set; }
        public virtual User JobSeeker { get; set; }
    }
}
