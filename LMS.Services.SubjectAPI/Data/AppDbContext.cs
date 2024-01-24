using LMS.Services.SubjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services.SubjectAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        // creating Subjects Table
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subject>().HasData(
                new Subject
                {
                    SubjectId = 1,
                    Name = "Introduction to Computer Science",
                    Code = "CS101",
                    Description = "An introductory course covering the fundamentals of computer science.",
                    Credits = 3
                },
                new Subject
                {
                    SubjectId = 2,
                    Name = "Web Development Basics",
                    Code = "WD200",
                    Description = "Basic concepts and skills for web development.",
                    Credits = 4
                },
                new Subject
                {
                    SubjectId = 3,
                    Name = "Database Management Systems",
                    Code = "DBMS301",
                    Description = "Study of database systems and SQL.",
                    Credits = 3
                },
                new Subject
                {
                    SubjectId = 4,
                    Name = "Algorithm Design",
                    Code = "ALGO401",
                    Description = "Advanced course on algorithm design and analysis.",
                    Credits = 4
                },
                new Subject
                {
                    SubjectId = 5,
                    Name = "Cybersecurity Fundamentals",
                    Code = "SEC101",
                    Description = "Introduction to cybersecurity principles and practices.",
                    Credits = 3
                },
                new Subject
                {
                    SubjectId = 6,
                    Name = "Data Structures",
                    Code = "DS201",
                    Description = "Study of fundamental data structures.",
                    Credits = 3
                },
                new Subject
                {
                    SubjectId = 7,
                    Name = "Software Engineering",
                    Code = "SE401",
                    Description = "Principles and practices of software engineering.",
                    Credits = 4
                },
                new Subject
                {
                    SubjectId = 8,
                    Name = "Artificial Intelligence",
                    Code = "AI501",
                    Description = "Exploration of artificial intelligence concepts and techniques.",
                    Credits = 4
                },
                new Subject
                {
                    SubjectId = 9,
                    Name = "Mobile App Development",
                    Code = "MAD301",
                    Description = "Development of mobile applications for various platforms.",
                    Credits = 3
                },
                new Subject
                {
                    SubjectId = 10,
                    Name = "Network Security",
                    Code = "NETSEC401",
                    Description = "Security aspects of computer networks.",
                    Credits = 4
                }
                );
        }
    }
}
