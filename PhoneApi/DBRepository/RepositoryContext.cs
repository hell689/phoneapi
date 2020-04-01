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

        public DbSet<CabinetPhone> CabinetPhones { get; set; }

        public DbSet<EmployeeCabinetPhone> EmployeeCabinetPhones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CabinetPhone>()
                .HasKey(t => new { t.Id });
            modelBuilder.Entity<CabinetPhone>()
                .HasOne(cp => cp.Cabinet)
                .WithMany(c => c.CabinetPhones)
                .HasForeignKey(cp => cp.CabinetId);
            modelBuilder.Entity<CabinetPhone>()
                .HasOne(cp => cp.Phone)
                .WithMany(p => p.CabinetPhones)
                .HasForeignKey(cp => cp.PhoneId);

            modelBuilder.Entity<EmployeeCabinetPhone>()
                .HasKey(t => new { t.EmployeeId, t.CabinetPhoneId });
            modelBuilder.Entity<EmployeeCabinetPhone>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeCabinetPhones)
                .HasForeignKey(ep => ep.EmployeeId);
            modelBuilder.Entity<EmployeeCabinetPhone>()
                .HasOne(ep => ep.CabinetPhone)
                .WithMany(p => p.EmployeeCabinetPhones)
                .HasForeignKey(ep => new { ep.CabinetPhoneId});
        }
    }
}
