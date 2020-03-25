using PhoneApi.DBRepository.Interfaces;
using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.DBRepository.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }
        public async Task AddEmployee(Employee employee)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Employees.Add(employee);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Employees.ToList();
            }
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Employees.FirstOrDefault(c => c.Id == employeeId);
            }
        }

        public async Task DeleteEmployee(int employeeId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var employee = new Employee() { Id = employeeId };
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
            }
        }
    }

}