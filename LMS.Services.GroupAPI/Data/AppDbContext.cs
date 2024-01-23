using LMS.Services.GroupAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services.GroupAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        // creating Groups Table
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>().HasData(
                new Group { GroupId = 1, Name = "716-21"},
                new Group { GroupId = 2, Name = "210-23"},
                new Group { GroupId = 3, Name = "713-21"}
                );
        }
    }
}
