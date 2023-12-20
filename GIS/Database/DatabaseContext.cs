using GIS.Models;
using Microsoft.EntityFrameworkCore;

namespace GIS.Database
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {

        }

        public DbSet<Sample> Samples { get; set; } = null!;
        public DbSet<Feedback> Feedbacks { get; set; } = null!; 
        public DbSet<Feedback> Materials { get; set; } = null!; 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sample>().HasKey(x => x.Id);
            modelBuilder.Entity<Feedback>().HasKey(x => x.Id); 
            modelBuilder.Entity<Material>().HasKey(x => x.Id);
        }
    }
}
