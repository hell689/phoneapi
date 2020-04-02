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
                    List<CabinetPhone> cabinetPhones = new List<CabinetPhone>();
                    List<EmployeeCabinetPhone> employeeCabinetPhones = context.EmployeeCabinetPhones.FromSqlRaw("SELECT employeeId, cabinetPhoneId FROM EmployeeCabinetPhones WHERE employeeId = {0}", employee.Id).ToList();
                    foreach (var employeeCabinetPhone in employeeCabinetPhones)
                    {
                        CabinetPhone cabinetPhone = context.CabinetPhones.FirstOrDefault(p => p.Id == employeeCabinetPhone.CabinetPhoneId);
                        cabinetPhone.EmployeeCabinetPhones = new List<EmployeeCabinetPhone>();
                        cabinetPhone.Cabinet = context.Cabinets.FirstOrDefault(c => c.Id == cabinetPhone.CabinetId);
                        cabinetPhone.Cabinet.CabinetPhones = new List<CabinetPhone>();
                        cabinetPhone.Phone = context.Phones.FirstOrDefault(p => p.Id == cabinetPhone.PhoneId);
                        cabinetPhone.Phone.CabinetPhones = new List<CabinetPhone>();
                        cabinetPhones.Add(cabinetPhone);
                    }
                    employee.EmployeeCabinetPhones = new List<EmployeeCabinetPhone>();
                    employee.CabinetPhones = cabinetPhones;

                }
                return employees;
            }
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                Employee employee = context.Employees.FirstOrDefault(c => c.Id == employeeId);

                List<CabinetPhone> cabinetPhones = new List<CabinetPhone>();
                List<EmployeeCabinetPhone> employeeCabinetPhones = context.EmployeeCabinetPhones.FromSqlRaw("SELECT employeeId, cabinetPhoneId FROM EmployeeCabinetPhones WHERE employeeId = {0}", employee.Id).ToList();
                foreach (var employeeCabinetPhone in employeeCabinetPhones)
                {
                    CabinetPhone cabinetPhone = context.CabinetPhones.FirstOrDefault(p => p.Id == employeeCabinetPhone.CabinetPhoneId);
                    cabinetPhone.EmployeeCabinetPhones = new List<EmployeeCabinetPhone>();
                    cabinetPhone.Cabinet = context.Cabinets.FirstOrDefault(c => c.Id == cabinetPhone.CabinetId);
                    cabinetPhone.Cabinet.CabinetPhones = new List<CabinetPhone>();
                    cabinetPhone.Phone = context.Phones.FirstOrDefault(p => p.Id == cabinetPhone.PhoneId);
                    cabinetPhone.Phone.CabinetPhones = new List<CabinetPhone>();
                    cabinetPhones.Add(cabinetPhone);
                }
                employee.EmployeeCabinetPhones = new List<EmployeeCabinetPhone>();
                employee.CabinetPhones = cabinetPhones;

                return employee;
            }
        }

        public async Task AddEmployeeToPhone(CabinetPhone cabinetPhone, Employee employee)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {

                context.Database.ExecuteSqlRaw("INSERT INTO EmployeeCabinetPhones (EmployeeId, CabinetPhoneId) VALUES ({0}, {1})", employee.Id, cabinetPhone.Id);
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

        public async Task DeletePhoneFromEmployee (Employee employee, CabinetPhone cabinetPhone)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Database.ExecuteSqlRaw("DELETE FROM EmployeeCabinetPhones WHERE CabinetPhoneId={0} AND EmployeeId={1}", cabinetPhone.Id, employee.Id);
                await context.SaveChangesAsync();
            }
            
    }
    }
    
}