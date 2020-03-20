using Microsoft.EntityFrameworkCore;
using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.DBRepository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }

        public DbSet<Phone> Phones { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CabinetPhone>()
                .HasKey(t => new { t.CabinetId, t.PhoneId });
            modelBuilder.Entity<CabinetPhone>()
                .HasOne(cp => cp.Cabinet)
                .WithMany(c => c.CabinetPhones)
                .HasForeignKey(cp => cp.CabinetId);
            modelBuilder.Entity<CabinetPhone>()
                .HasOne(cp => cp.Phone)
                .WithMany(p => p.CabinetPhones)
                .HasForeignKey(cp => cp.PhoneId);

            modelBuilder.Entity<EmployeePhone>()
                .HasKey(t => new { t.EmployeeId, t.PhoneId });
            modelBuilder.Entity<EmployeePhone>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeePhones)
                .HasForeignKey(ep => ep.EmployeeId);
            modelBuilder.Entity<EmployeePhone>()
                .HasOne(ep => ep.Phone)
                .WithMany(p => p.EmployeePhones)
                .HasForeignKey(ep => ep.PhoneId);
        }
    }
}
