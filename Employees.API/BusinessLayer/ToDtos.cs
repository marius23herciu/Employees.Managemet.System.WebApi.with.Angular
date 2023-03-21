using Employees.API.Data;
using Employees.API.DTOs;
using Employees.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.API.BusinessLayer
{
    public class ToDtos
    {
        private readonly EmployeesDbContext _context;
        public ToDtos(EmployeesDbContext context)
        {
            this._context = context;
        }

        public async Task<DepartmentDto> DepartmentToDto(Department department)
        {
            var departmentDto = new DepartmentDto
            {
                Name = department.Name,
                NoOfEmployees = department.Employees.Count()
            };
            return departmentDto;
        }
        public async Task<EmployeeByDepartmentDto> EmployeeByDepartmentToDto(Employee employee)
        {
            var employeeDto = new EmployeeByDepartmentDto
            {
                Id = (Guid)employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                Department = GetDepartmentsName(employee.Id.Value).Result,
                Email = employee.Email,
                Phone = employee.Phone

            };
            return employeeDto;
        }
        public async Task<EmployeeDto> EmployeeToDto(Employee employee)
        {
            var employeeDto = new EmployeeDto
            {
                Id = (Guid)employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DOB = new DayMonthYearDto
                {
                    Day = employee.DOB.Day,
                    Month = employee.DOB.Month,
                    Year = employee.DOB.Year
                },
                Address = new AddressDto
                {
                    City = employee.Address.City,
                    Street = employee.Address.Street,
                    Number = employee.Address.Number
                },
                Salary = employee.Salary,
                Department = GetDepartmentsName(employee.Id.Value).Result,
                Email = employee.Email,
                Phone = employee.Phone

            };
            return employeeDto;
        }
        public async Task<string> GetDepartmentsName(Guid id)
        {
            var departments = await _context.Departments.Include(e => e.Employees).ToListAsync();

            foreach (var dep in departments)
            {
                var employeeOfDepartment = dep.Employees.FirstOrDefault(d => d.Id == id);
                if (employeeOfDepartment != null)
                {
                    return dep.Name;
                }
            }

            return null;
        }

    }
}
