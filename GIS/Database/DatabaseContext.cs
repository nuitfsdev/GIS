﻿using GIS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public DbSet<DamageReport> DamageReports { get; set; } = null!;

        public DbSet<Notification> Notifications { get; set; } = null!;

        public DbSet<BodyMaterial> BodyMaterial { get; set; } = null!;
        public DbSet<Face> Face { get; set; } = null!;
        public DbSet<Node> Node { get; set; } = null!;
        public DbSet<FaceNode> FaceNode { get; set; } = null!;
        public DbSet<BodyRepairStatus> BodyRepairStatus { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sample>().HasKey(x => x.Id);
            modelBuilder.Entity<Account>().HasKey(x => x.Id);
            modelBuilder.Entity<Account>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<DamageReport>().HasKey(x => x.Id);
            modelBuilder.Entity<Notification>().HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(x => new { x.UserId, x.RoleId });
            //modelBuilder.Entity<AccountRole>().HasOne(x => x.Account).WithMany(x => x.AccountRoles).HasForeignKey(x => x.UserId);
            //modelBuilder.Entity<AccountRole>().HasOne(x => x.Role).WithMany(x => x.AccountRoles).HasForeignKey(x => x.RoleId);
            //modelBuilder.Entity<AccountRole>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<DamageReport>().HasOne(dr => dr.Body).WithMany(b => b.DamageReports).HasForeignKey(dr => dr.BodyId);
            modelBuilder.Entity<DamageReport>().HasOne(dr => dr.Account).WithMany(b => b.DamageReports).HasForeignKey(dr => dr.AccountId);
            modelBuilder.Entity<Feedback>().HasKey(x => x.Id); 
            modelBuilder.Entity<Material>().HasKey(x => x.Id);
            modelBuilder.Entity<Body>().ToTable("Body").HasKey(x => x.Id);
            modelBuilder.Entity<BodyComp>().ToTable("BodyComp");
            modelBuilder.Entity<Prism>().ToTable("Prism");
            modelBuilder.Entity<BodyMaterial>().Ignore(x => x.Id);
            modelBuilder.Entity<BodyMaterial>().HasOne(x => x.Body).WithOne(x => x.BodyMaterial).HasForeignKey<BodyMaterial>(x => x.BodyId);
            modelBuilder.Entity<BodyMaterial>().HasOne(x => x.Material).WithMany(x => x.BodyMaterial).HasForeignKey(x => x.MaterialId);
            modelBuilder.Entity<BodyMaterial>().HasKey(x => new { x.BodyId, x.MaterialId });
            modelBuilder.Entity<Face>().HasKey(x => x.Id);
            modelBuilder.Entity<Node>().HasKey(x => x.Id);
            modelBuilder.Entity<FaceNode>().HasOne(x => x.Node).WithMany(x => x.FaceNode).HasForeignKey(x => x.NodeId);
            modelBuilder.Entity<FaceNode>().HasOne(x => x.Face).WithMany(x => x.FaceNode).HasForeignKey(x => x.FaceId);
            modelBuilder.Entity<BodyRepairStatus>().HasKey(x => x.Id);
            modelBuilder.Entity<BodyRepairStatus>().HasOne(x => x.Account).WithMany(x => x.BodyRepairStatuses).HasForeignKey(x => x.AccountId);
            modelBuilder.Entity<BodyRepairStatus>().HasOne(x => x.Body).WithMany(x => x.BodyRepairStatus).HasForeignKey(x => x.BodyId);
            modelBuilder.Entity<BodyRepairStatus>().HasOne(x => x.DamageReport).WithOne(x => x.BodyRepairStatus).HasForeignKey<BodyRepairStatus>(x => x.DamageReportId);
        }
    }
}
