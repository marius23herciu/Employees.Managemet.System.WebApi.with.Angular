using System.ComponentModel.DataAnnotations;

namespace Employees.API.DTOs
{
    public class DepartmentDto
    {
        [Required(ErrorMessage = "FirstName is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Number of employees is required.")]
        public int NoOfEmployees { get; set; }
    }
}
