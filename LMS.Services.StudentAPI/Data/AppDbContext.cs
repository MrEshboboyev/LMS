using LMS.Services.StudentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services.StudentAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        // creating Students Table
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, GroupId = 1, Name = "John Doe"},
                new Student { StudentId = 2, GroupId = 2, Name = "Jack Sparrow"},
                new Student { StudentId = 3, GroupId = 3, Name = "Albert Einstein" },
                new Student { StudentId = 4, GroupId = 2, Name = "Nikola Tesla" }
                );
        }
    }
}
