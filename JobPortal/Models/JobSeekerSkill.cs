using System;
using System.Collections.Generic;

#nullable disable

namespace JobPortal.Models
{
    public partial class JobSeekerSkill
    {
        public int Id { get; set; }
        public int? JobSeekerId { get; set; }
        public string SkillName { get; set; }

        public virtual User JobSeeker { get; set; }
    }
}
