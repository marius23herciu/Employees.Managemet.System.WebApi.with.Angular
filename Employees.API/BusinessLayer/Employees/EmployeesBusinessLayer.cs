using Employees.API.Data;
using Employees.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employees.API.DTOs;
using System.Net;
using Employees.API.BusinessLayer;

namespace Employees.API.BussinesLayer.Employees
{
    public class EmployeesBusinessLayer
    {
        private readonly EmployeesDbContext _context;
        private readonly ToDtos _toDtos;

        public EmployeesBusinessLayer(EmployeesDbContext context, ToDtos toDtos)
        {
            this._context = context;
            this._toDtos = toDtos;
        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var employees = await _context.Employees.Include(a => a.Address).Include(d => d.DOB).OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToListAsync();

            var employeesDtos = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                var employeeDto = _toDtos.EmployeeToDto(employee).Result;
                employeesDtos.Add(employeeDto);
            }
            return employeesDtos;
        }
        public async Task<EmployeeDto> GetEmployee(Guid id)
        {
            var employee = await _context.Employees.Include(a => a.Address).Include(d => d.DOB).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return null;
            }
            var employeeDto = _toDtos.EmployeeToDto(employee).Result;

            return employeeDto;
        }
        public async Task<EmployeeDto> AddEmployee(EmployeeDto employee)
        {
            var newEmployee = new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DOB = new DayMonthYear
                {
                    Day = employee.DOB.Day,
                    Month = employee.DOB.Month,
                    Year = employee.DOB.Year
                },
                Address = new Address
                {
                    City = employee.Address.City,
                    Street = employee.Address.Street,
                    Number = employee.Address.Number,
                },
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
            };

            var employeesDepartment = _context.Departments.FirstOrDefaultAsync(d => d.Name == employee.Department).Result;

            if (employeesDepartment == null)
            {
                employeesDepartment = new Department
                {
                    Name = employee.Department
                };
                employeesDepartment.Employees.Add(newEmployee);
                await _context.Departments.AddAsync(employeesDepartment);
            }
            else
            {
                employeesDepartment.Employees.Add(newEmployee);
            }


            await _context.AddAsync(newEmployee);
            await _context.SaveChangesAsync();

            return _toDtos.EmployeeToDto(newEmployee).Result;
        }

        public async Task<EmployeeDto> UpdateEmployee(Guid id, EmployeeDto editEmployee)
        {
            var employee = await _context.Employees.Include(a => a.Address).Include(d => d.DOB).FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return null;
            }

            employee.FirstName = editEmployee.FirstName;
            employee.LastName = editEmployee.LastName;
            employee.Email = editEmployee.Email;
            employee.Phone = editEmployee.Phone;
            employee.Salary = editEmployee.Salary;
            employee.Address.City = editEmployee.Address.City;
            employee.Address.Street = editEmployee.Address.Street;
            employee.Address.Number = editEmployee.Address.Number;
            employee.DOB.Day = editEmployee.DOB.Day;
            employee.DOB.Month = editEmployee.DOB.Month;
            employee.DOB.Year = editEmployee.DOB.Year;

            var alldDepartments = await _context.Departments.ToListAsync();
            foreach (var dep in alldDepartments)
            {
                if (dep.Employees.Contains(employee))
                {
                    dep.Employees.Remove(employee);
                }
            }


            bool employeeIsAddedToDepartment = false;
            foreach (var dep in alldDepartments)
            {
                if (dep.Name == editEmployee.Department)
                {
                    dep.Employees.Add(employee);
                    employeeIsAddedToDepartment = true;
                    break;
                }
            }

            if (!employeeIsAddedToDepartment)
            {
                var employeesDepartment = new Department
                {
                    Name = editEmployee.Department
                };
                employeesDepartment.Employees.Add(employee);
                await _context.Departments.AddAsync(employeesDepartment);
            }

            await _context.SaveChangesAsync();

            return _toDtos.EmployeeToDto(employee).Result;
        }
        public async Task<bool> DeleteEmployee(Guid id)
        {

            var employee = await _context.Employees.Include(a => a.Address).Include(d => d.DOB).FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return false;
            }

            var allDepartments = await _context.Departments.ToListAsync();
            foreach (var dep in allDepartments)
            {
                if (dep.Employees.Contains(employee))
                {
                    dep.Employees.Remove(employee);
                }
            }

            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.EmployeeId == id);
            address.EmployeeId = new Guid();
            _context.Remove(address);

            var DOB = await _context.Dates.FirstOrDefaultAsync(a => a.EmployeeId == id);
            DOB.EmployeeId = new Guid();
            _context.Remove(DOB);

            _context.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
