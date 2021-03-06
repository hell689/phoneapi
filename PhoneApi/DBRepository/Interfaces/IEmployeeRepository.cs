﻿using PhoneApi.Models;
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
        Task AddEmployeeToPhone(CabinetPhone cabinetPhone, Employee employee);
        Task DeletePhoneFromEmployee(Employee employee, CabinetPhone cabinetPhone);
        Task DeleteEmployee(int employeeId);
    }
}
