using Employees.API.BusinessLayer.Departments;
using Employees.API.Data;
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
        public async Task<IActionResult> AddDepartment([FromBody] string depName)
        {
            var department = await _bussinesLayer.AddDepartment(depName);
            if (department == null)
            {
                return BadRequest($"Department {depName} allready exists.");
            }

            return Ok(department);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeDepartmentsName([FromBody] string oldName, string newName)
        {
            var department = await _bussinesLayer.ChangeName(oldName, newName);
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
