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
        Task DeleteEmployee(int Id);
    }
}
