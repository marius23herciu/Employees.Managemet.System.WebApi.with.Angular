using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employees.API.Models
{
    public class DayMonthYear
    {
        public int? Id { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [ForeignKey("Employee")]
        public Guid? EmployeeId { get; set; }
    }
}
