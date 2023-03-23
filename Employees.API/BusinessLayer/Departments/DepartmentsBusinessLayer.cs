using Employees.API.Data;
using Employees.API.DTOs;
using Employees.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace Employees.API.BusinessLayer.Departments
{
    public class DepartmentsBusinessLayer
    {
        private readonly EmployeesDbContext _context;
        private readonly ToDtos _toDtos;

        public DepartmentsBusinessLayer(EmployeesDbContext context, ToDtos toDtos)
        {
            this._context = context;
            this._toDtos = toDtos;
        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            var departments = await _context.Departments.Include(e => e.Employees).OrderBy(n => n.Name).ToListAsync();

            var departmentsDtos = new List<DepartmentDto>();
            foreach (var dep in departments)
            {
                var employeeDto = _toDtos.DepartmentToDto(dep).Result;
                departmentsDtos.Add(employeeDto);
            }
            return departmentsDtos;
        }
        public async Task<DepartmentDto> GetDepartment(string depName)
        {
            var department = await _context.Departments.Include(e => e.Employees).FirstOrDefaultAsync(n => n.Name == depName);
            if (department == null)
            {
                return null;
            }

            var departmentsDto = _toDtos.DepartmentToDto(department).Result;
            return departmentsDto;
        }
        public async Task<List<EmployeeByDepartmentDto>> GetEmployeesByDepartment(string depName)
        {
            var department = await _context.Departments.Include(e => e.Employees).FirstOrDefaultAsync(n => n.Name == depName);
            if (department == null)
            {
                return null;
            }

            var employees = department.Employees;
            var employeesDtos = new List<EmployeeByDepartmentDto>();
            foreach (var employee in employees)
            {
                var employeeDto = _toDtos.EmployeeByDepartmentToDto(employee).Result;
                employeesDtos.Add(employeeDto);
            }
            return employeesDtos;
        }
        public async Task<DepartmentDto> AddDepartment(string depName)
        {
            var checkDepartment = await _context.Departments.FirstOrDefaultAsync(n => n.Name == depName);
            if (checkDepartment != null)
            {
                return null;
            }

            var department = new Department
            {
                Name = depName
            };

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            var departmentToDto = _toDtos.DepartmentToDto(department).Result;

            return departmentToDto;
        }

        public async Task<DepartmentDto> ChangeName(string oldName, string newName)
        {
            var checkOldDepartment = await _context.Departments.Include(e => e.Employees).FirstOrDefaultAsync(n => n.Name == oldName);
            if (checkOldDepartment == null)
            {
                return null;
            }

            var checkNewDepartment = await _context.Departments.Include(e => e.Employees).FirstOrDefaultAsync(n => n.Name == newName);
            if (checkNewDepartment != null)
            {
                return null;
            }

            checkOldDepartment.Name = newName;

            await _context.SaveChangesAsync();

            var departmentToDto = _toDtos.DepartmentToDto(checkOldDepartment).Result;

            return departmentToDto;
        }

        public async Task<bool> DeleteDepartment(string depName)
        {
            var department = await _context.Departments.Include(e => e.Employees).FirstOrDefaultAsync(d => d.Name == depName);
            if (department == null)
            {
                return false;
            }

            var numberOfEmployeesForThisDepartment = department.Employees.ToList();
            if (numberOfEmployeesForThisDepartment.Count > 0)
            {
                return false;
            }

            _context.Remove(department);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
