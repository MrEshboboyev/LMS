using LMS.Services.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services.Shared.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        // creating Subjects Table
        public DbSet<GroupSubject> GroupSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GroupSubject>().HasData(
               new GroupSubject { GroupSubjectId = 1, GroupId = 1, SubjectId = 1 },
               new GroupSubject { GroupSubjectId = 2, GroupId = 2, SubjectId = 2 },
               new GroupSubject { GroupSubjectId = 3, GroupId = 9, SubjectId = 3 },
               new GroupSubject { GroupSubjectId = 4, GroupId = 2, SubjectId = 4 },
               new GroupSubject { GroupSubjectId = 5, GroupId = 3, SubjectId = 5 },
               new GroupSubject { GroupSubjectId = 7, GroupId = 1, SubjectId = 10 },
               new GroupSubject { GroupSubjectId = 8, GroupId = 3, SubjectId = 4 },
               new GroupSubject { GroupSubjectId = 9, GroupId = 3, SubjectId = 2 },
               new GroupSubject { GroupSubjectId = 10, GroupId = 2, SubjectId = 9 }
                );
        }
    }
}
