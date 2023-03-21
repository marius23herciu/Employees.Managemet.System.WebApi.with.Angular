using Employees.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Employees.API.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DayMonthYearDto DOB { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        public long Phone { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public AddressDto Address { get; set; }
        [Required(ErrorMessage = "Salary is required.")]
        public long Salary { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }
    }
}
