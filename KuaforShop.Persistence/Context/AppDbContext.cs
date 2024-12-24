using Microsoft.EntityFrameworkCore;
using KuaforShop.Core;
using KuaforShop.Core.Entities;

namespace KuaforShop.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Appointments> Appointments { get; set; }

        public DbSet<Employees> Employees { get; set; }

        public DbSet<Expertise> Expertise { get; set; }

        public DbSet<Saloons> Saloons { get; set; }

        public DbSet<Services> Services { get; set; }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId);

            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
