using Microsoft.Extensions.Configuration;
using PhoneApi.DBRepository.Interfaces;
using PhoneApi.Models;
using PhoneApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Services
{
    public class EmployeeService : IEmployeeService
    { 

        IEmployeeRepository _repository;
        IConfiguration _config;

        public EmployeeService(IEmployeeRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }
        public async Task AddEmployee(Employee employee)
        {
            await _repository.AddEmployee(employee);
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var cabinets = await _repository.GetAllEmployees();
            return cabinets;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            var result = await _repository.GetEmployee(Id);
            return result;
        }

        public async Task DeleteEmployee(int Id)
        {
            await _repository.DeleteEmployee(Id);
        }

        public async Task AddEmployeeToPhone(Phone phone, Employee employee)
        {
            await _repository.AddEmployeeToPhone(phone, employee);
        }

        public async Task DeletePhoneFromEmployee(Employee employee, Phone phone)
        {
            await _repository.DeletePhoneFromEmployee(employee, phone);
        }
    }
}
