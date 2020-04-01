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
        private readonly ICabinetPhoneService cabinetPhoneService;

        public EmployeeController(ICabinetPhoneService cabinetPhoneService, IEmployeeService service)
        {
            employeeService = service;
            this.cabinetPhoneService = cabinetPhoneService;
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

        [HttpPost("{employeeId}/{cabinetPhoneId}")]
        public async Task AddEmployeeToPhone(int employeeId, int cabinetPhoneId)
        {
            CabinetPhone cabinetPhone = await cabinetPhoneService.GetCabinetPhone(cabinetPhoneId);
            Employee employee = await employeeService.GetEmployee(employeeId);
            await employeeService.AddEmployeeToPhone(cabinetPhone, employee);
        }

        [HttpDelete("{employeeId}/{cabinetPhoneId}")]
        public async Task<ActionResult<Employee>> DeleteCabinetFromPhone(int employeeId, int cabinetPhoneId)
        {
            CabinetPhone cabinetPhone = await cabinetPhoneService.GetCabinetPhone(cabinetPhoneId);
            Employee employee = await employeeService.GetEmployee(employeeId);
            if (employee == null)
            {
                return BadRequest();
            }
            await employeeService.DeletePhoneFromEmployee(employee, cabinetPhone);
            return Ok(employee);
        }
    }
}
