using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace JobPortal.Models
{
    public partial class JobSeekerEducation
    {
        public int Id { get; set; }
        public int? JobSeekerId { get; set; }

        [MinLength(1)]
        public string Education { get; set; }

        [MinLength(1)]
        public string Board { get; set; }

        [MinLength(1)]
        public string Course { get; set; }

        public int? PassingYear { get; set; }

        [MinLength(1)]
        public string University { get; set; }

        [MinLength(1)]
        public string CourseType { get; set; }

        public int? Marks { get; set; }

        public virtual User JobSeeker { get; set; }
    }
}
