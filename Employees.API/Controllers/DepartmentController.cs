using Employees.API.BusinessLayer.Departments;
using Employees.API.Data;
using Employees.API.DTOs;
using Employees.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EmployeesDbContext _context;
        private readonly DepartmentsBusinessLayer _bussinesLayer;
        public DepartmentController(EmployeesDbContext employeesDbContext, DepartmentsBusinessLayer businessLayer)
        {
            this._context = employeesDbContext;
            this._bussinesLayer = businessLayer;
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            var department = await _bussinesLayer.AddDepartment(departmentDto.Name);
            if (department == null)
            {
                return BadRequest($"Department {departmentDto.Name} allready exists.");
            }

            return Ok(department);
        }

        [HttpPut]
        [Route("{oldName}")]
        public async Task<IActionResult> ChangeDepartmentsName([FromRoute] string oldName, [FromBody] DepartmentDto departmentDto)
        {
            var department = await _bussinesLayer.ChangeName(oldName, departmentDto.Name);
            if (department == null)
            {
                return NotFound($"Department {oldName} not found.");
            }

            return Ok(department);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            return Ok(await _bussinesLayer.GetDepartments());
        }

        [HttpGet]
        [Route("{depName}")]
        public async Task<IActionResult> GetDepartment([FromRoute] string depName)
        {
            var changedDep = await _bussinesLayer.GetDepartment(depName);
            if (changedDep == null)
            {
                return BadRequest();
            }
            return Ok(changedDep);
        }
        [HttpGet]
        [Route("get-employees-from-{depName}")]
        public async Task<IActionResult> GetEmployeesByDepartment([FromRoute] string depName)
        {
            var employees = await _bussinesLayer.GetEmployeesByDepartment(depName);

            if (employees == null)
            {
                return NotFound("Department not found.");
            }

            return Ok(employees);
        }
        [HttpDelete]
        [Route("{depName}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] string depName)
        {
            var department = await _bussinesLayer.DeleteDepartment(depName);
            if (department == false)
            {
                return BadRequest("Department not found or has employees.");
            }

            return Ok();
        }
    }
}
