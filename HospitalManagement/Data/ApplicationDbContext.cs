namespace HospitalManagement.Data
{
    using HospitalManagement.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Assistant> Assistants { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<InstructorSchedule> InstructorSchedules { get; set; }
        public DbSet<AssistantSchedule> AssistantSchedules { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Emergency> Emergencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InstructorSchedule>()
                .Property(s => s.day)
                .HasConversion<string>(); // Save enum as string
            modelBuilder.Entity<InstructorSchedule>()
              .Property(s => s.shift)
              .HasConversion<string>(); // Save enum as string
            modelBuilder.Entity<AssistantSchedule>()
               .Property(s => s.day)
               .HasConversion<string>(); // Save enum as string
            modelBuilder.Entity<AssistantSchedule>()
              .Property(s => s.shift)
              .HasConversion<string>(); // Save enum as string


        }

    }
}
