using GIS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GIS.Database
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {

        }

        public DbSet<Sample> Samples { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<IdentityUserRole<Guid>> IdentityUserRoles { get; set; } = null!;

        public DbSet<Feedback> Feedbacks { get; set; } = null!; 
        public DbSet<Material> Materials { get; set; } = null!; 
        public DbSet<Body> Body { get; set; } = null!;
        public DbSet<BodyComp> BodyComp { get; set; } = null!;
        public DbSet<Prism> Prism { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sample>().HasKey(x => x.Id);
            modelBuilder.Entity<Account>().HasKey(x => x.Id);
            modelBuilder.Entity<Account>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(x => new { x.UserId, x.RoleId });
            //modelBuilder.Entity<AccountRole>().HasOne(x => x.Account).WithMany(x => x.AccountRoles).HasForeignKey(x => x.UserId);
            //modelBuilder.Entity<AccountRole>().HasOne(x => x.Role).WithMany(x => x.AccountRoles).HasForeignKey(x => x.RoleId);
            //modelBuilder.Entity<AccountRole>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<Feedback>().HasKey(x => x.Id); 
            modelBuilder.Entity<Material>().HasKey(x => x.Id);
            modelBuilder.Entity<Body>().ToTable("Body").HasKey(x => x.Id);
            modelBuilder.Entity<BodyComp>().ToTable("BodyComp");
            modelBuilder.Entity<Prism>().ToTable("Prism");
        }
    }
}
