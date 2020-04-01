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
        IPhoneService _phoneService;

        public EmployeeService(IEmployeeRepository repository, IConfiguration configuration, IPhoneService phoneService)
        {
            _repository = repository;
            _config = configuration;
            _phoneService = phoneService;
        }
        public async Task AddEmployee(Employee employee)
        {
            await _repository.AddEmployee(employee);
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var employees = await _repository.GetAllEmployees();
            /*foreach (Employee employee in employees)
            {
                foreach (Phone phone in employee.Phones)
                {
                    Phone _phone = await _phoneService.GetPhone(unchecked((int)phone.Id));
                    phone.Cabinets = _phone.Cabinets;
                }
            }*/
            return employees;
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

        public async Task AddEmployeeToPhone(CabinetPhone cabinetPhone, Employee employee)
        {
            await _repository.AddEmployeeToPhone(cabinetPhone, employee);
        }

        public async Task DeletePhoneFromEmployee(Employee employee, CabinetPhone cabinetPhone)
        {
            await _repository.DeletePhoneFromEmployee(employee, cabinetPhone);
        }
    }
}
