using Microsoft.EntityFrameworkCore;
using NerxeasWeb.Models;

namespace NerxeasWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Creating the DbContext and adding it some options. Those options are builded in Program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Creating a table...
        public DbSet<Category> Categories { get; set; }

        // Seeding some dummy data to our Database/Table to test it via EntityFrameworkCore.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 123 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 456 },
                new Category { Id = 3, Name = "History", DisplayOrder = 789 }
                );
        }
    }
}
