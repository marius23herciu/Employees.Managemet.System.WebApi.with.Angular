using System.ComponentModel.DataAnnotations.Schema;

namespace Employees.API.Models
{
    public class Address
    {
        public int? Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        [ForeignKey("Employee")]
        public Guid? EmployeeId { get; set; }
    }
}
