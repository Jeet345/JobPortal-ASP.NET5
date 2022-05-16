using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortal.Models
{
    public partial class JobSeekerEducation
    {
        public int Id { get; set; }
        public int? JobSeekerId { get; set; }
        public string Education { get; set; }
        public string Board { get; set; }
        public string Course { get; set; }
        public int? PassingYear { get; set; }
        public string University { get; set; }
        public string CourseType { get; set; }
        public int? Marks { get; set; }

        public virtual User JobSeeker { get; set; }
    }
}
