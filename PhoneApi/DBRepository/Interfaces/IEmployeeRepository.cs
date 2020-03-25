using PhoneApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.DBRepository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployee(int employeeId);
        Task AddEmployee(Employee employee);
        Task<List<Employee>> GetAllEmployees();
        Task AddEmployeeToPhone(Phone phone, Employee employee);
        Task DeletePhoneFromEmployee(Employee employee, Phone phone);
        Task DeleteEmployee(int employeeId);
    }
}
