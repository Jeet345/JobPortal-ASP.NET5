using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace JobPortal.Models
{
    public partial class JobPortalDBContext : DbContext
    {
        public JobPortalDBContext()
        {
        }

        public JobPortalDBContext(DbContextOptions<JobPortalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }
        public virtual DbSet<JobSeekerEducation> JobSeekerEducations { get; set; }
        public virtual DbSet<JobSeekerExperience> JobSeekerExperiences { get; set; }
        public virtual DbSet<JobSeekerProject> JobSeekerProjects { get; set; }
        public virtual DbSet<JobSeekerSkill> JobSeekerSkills { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=jeet;Initial Catalog=JobPortalDB;Integrated Security=True;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.GroupName).IsUnicode(false);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.JobApplications).HasDefaultValueSql("((0))");

                entity.Property(e => e.JobEducation).IsUnicode(false);

                entity.Property(e => e.JobExperience).IsUnicode(false);

                entity.Property(e => e.JobLocation).IsUnicode(false);

                entity.Property(e => e.JobPostingDate).IsUnicode(false);

                entity.Property(e => e.JobSkills).IsUnicode(false);

                entity.Property(e => e.JobStatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.JobTitle).IsUnicode(false);

                entity.Property(e => e.JobType).IsUnicode(false);

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("FK_Job_User");

                entity.HasOne(d => d.JobCategory)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.JobCategoryId)
                    .HasConstraintName("FK_Job_Category");
            });

            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.ToTable("JobApplication");

                entity.Property(e => e.ApplicationDate).IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.JobApplications)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_JobApplication_Category");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.JobApplicationEmployers)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("FK_JobApplications_JobApplications");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobApplicationsNavigation)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_JobApplication_Job");

                entity.HasOne(d => d.JobSeeker)
                    .WithMany(p => p.JobApplicationJobSeekers)
                    .HasForeignKey(d => d.JobSeekerId)
                    .HasConstraintName("FK_JobApplication_User");
            });

            modelBuilder.Entity<JobSeekerEducation>(entity =>
            {
                entity.ToTable("JobSeekerEducation");

                entity.Property(e => e.Board).IsUnicode(false);

                entity.Property(e => e.Course).IsUnicode(false);

                entity.Property(e => e.CourseType).IsUnicode(false);

                entity.Property(e => e.Education).IsUnicode(false);

                entity.Property(e => e.University).IsUnicode(false);

                entity.HasOne(d => d.JobSeeker)
                    .WithMany(p => p.JobSeekerEducations)
                    .HasForeignKey(d => d.JobSeekerId)
                    .HasConstraintName("FK_JobSeekerEducation_User");
            });

            modelBuilder.Entity<JobSeekerExperience>(entity =>
            {
                entity.ToTable("JobSeekerExperience");

                entity.Property(e => e.Designation).IsUnicode(false);

                entity.Property(e => e.Experience).IsUnicode(false);

                entity.Property(e => e.JobProfile).IsUnicode(false);

                entity.Property(e => e.Organization).IsUnicode(false);

                entity.HasOne(d => d.JobSeeker)
                    .WithMany(p => p.JobSeekerExperiences)
                    .HasForeignKey(d => d.JobSeekerId)
                    .HasConstraintName("FK_JobSeekerExperience_JobSeekerExperience");
            });

            modelBuilder.Entity<JobSeekerProject>(entity =>
            {
                entity.ToTable("JobSeekerProject");

                entity.Property(e => e.ProjectClient).IsUnicode(false);

                entity.Property(e => e.ProjectDetails).IsUnicode(false);

                entity.Property(e => e.ProjectSkilles).IsUnicode(false);

                entity.Property(e => e.ProjectStatus).IsUnicode(false);

                entity.Property(e => e.ProjectTitle).IsUnicode(false);

                entity.HasOne(d => d.JobSeeker)
                    .WithMany(p => p.JobSeekerProjects)
                    .HasForeignKey(d => d.JobSeekerId)
                    .HasConstraintName("FK_JobSeekerProject_JobSeekerProject");
            });

            modelBuilder.Entity<JobSeekerSkill>(entity =>
            {
                entity.Property(e => e.SkillName).IsUnicode(false);

                entity.HasOne(d => d.JobSeeker)
                    .WithMany(p => p.JobSeekerSkills)
                    .HasForeignKey(d => d.JobSeekerId)
                    .HasConstraintName("FK_JobSeekerSkills_JobSeekerSkills");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.AboutCompany).IsUnicode(false);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.CompanyName).IsUnicode(false);

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.Languages).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Resume).IsUnicode(false);

                entity.Property(e => e.State).IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Website).IsUnicode(false);

                entity.Property(e => e.WorkStatus).IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_User_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
