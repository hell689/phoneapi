using Microsoft.EntityFrameworkCore;
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
                List<Employee> employees = context.Employees.ToList();
                foreach (Employee employee in employees)
                {
                    List<Phone> phones = new List<Phone>();
                    List<EmployeePhone> employeePhones = context.EmployeePhones.FromSqlRaw("SELECT employeeId, phoneId FROM EmployeePhone WHERE employeeId = {0}", employee.Id).ToList();
                    foreach (var employeePhone in employeePhones)
                    {
                        Phone phone = context.Phones.FirstOrDefault(p => p.Id == employeePhone.PhoneId);
                        phone.EmployeePhones = new List<EmployeePhone>();
                        phones.Add(phone);
                    }
                    employee.EmployeePhones = new List<EmployeePhone>();
                    employee.Phones = phones;

                }
                return employees;
            }
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                Employee employee = context.Employees.FirstOrDefault(c => c.Id == employeeId);

                List<Phone> phones = new List<Phone>();
                List<EmployeePhone> employeePhones = context.EmployeePhones.FromSqlRaw("SELECT employeeId, phoneId FROM EmployeePhone WHERE employeeId = {0}", employee.Id).ToList();
                foreach (var employeePhone in employeePhones)
                {
                    Phone phone = context.Phones.FirstOrDefault(p => p.Id == employeePhone.PhoneId);
                    phone.EmployeePhones = new List<EmployeePhone>();
                    phones.Add(phone);
                }
                employee.EmployeePhones = new List<EmployeePhone>();
                employee.Phones = phones;

                return employee;
            }
        }

        public async Task AddEmployeeToPhone(Phone phone, Employee employee)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {

                context.Database.ExecuteSqlRaw("INSERT INTO EmployeePhone (EmployeeId, PhoneId) VALUES ({0}, {1})", employee.Id, phone.Id);
                await context.SaveChangesAsync();
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

        public async Task DeletePhoneFromEmployee (Employee employee, Phone phone)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Database.ExecuteSqlRaw("DELETE FROM EmployeePhone WHERE PhoneId={0} AND EmployeeId={1}", phone.Id, employee.Id);
                await context.SaveChangesAsync();
            }
        }
    }

}