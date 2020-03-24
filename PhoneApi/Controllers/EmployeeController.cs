using Microsoft.AspNetCore.Mvc;
using PhoneApi.Models;
using PhoneApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService service)
        {
            employeeService = service;
        }

        [HttpGet]
        public async Task<List<Employee>> GetEmployees()
        {
            return await employeeService.GetAllEmployees();
        }

        [HttpGet("{id}")]
        public async Task<Employee> GetEmployee(int id)
        {
            return await employeeService.GetEmployee(id);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            await employeeService.AddEmployee(employee);
            return Ok(employee);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            Employee employee = await employeeService.GetEmployee(id);
            if (employee == null)
            {
                return BadRequest();
            }
            await employeeService.DeleteEmployee(id);
            return Ok(employee);
        }
    }
}
