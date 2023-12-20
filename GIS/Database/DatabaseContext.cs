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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sample>().HasKey(x => x.Id);
        }
    }
}
