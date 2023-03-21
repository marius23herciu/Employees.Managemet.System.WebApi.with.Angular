using Employees.API.BusinessLayer;
using Employees.API.BussinesLayer.Employees;
using Employees.API.Data;
using Employees.API.DTOs;
using Employees.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Employees.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeesBusinessLayer _bussinesLayer;
        private readonly ToDtos _toDtos;
        public EmployeesController(EmployeesBusinessLayer bussinesLayer, ToDtos toDtos)
        {
            this._bussinesLayer = bussinesLayer;
            this._toDtos = toDtos;
        }

        //[HttpGet, Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _bussinesLayer.GetEmployees());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _bussinesLayer.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet]
        [Route("get-department-for-{id}")]
        public async Task<IActionResult> GetDepartmentsName([FromRoute] Guid id)
        {
            var depName = await _toDtos.GetDepartmentsName(id);

            if (depName == null)
            {
                return NotFound();
            }

            return Ok(depName);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employee)
        {
            var newEmployee = await _bussinesLayer.AddEmployee(employee);

            return Ok(newEmployee);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateEmployeesDetails([FromRoute] Guid id, [FromBody] EmployeeDto editEmployee)
        {
            var employee = await _bussinesLayer.UpdateEmployee(id, editEmployee);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var isEmployeeDeleted = await _bussinesLayer.DeleteEmployee(id);

            if (!isEmployeeDeleted)
            {
                return NotFound("Employee not found.");
            }

            return Ok();
        }

    }
}
