using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployee(int id);
        Task AddEmployee(Employee employee);
        Task<List<Employee>> GetAllEmployees();
        Task AddEmployeeToPhone(Phone phone, Employee employee);
        Task DeletePhoneFromEmployee(Employee employee, Phone phone);
        Task DeleteEmployee(int Id);
    }
}
