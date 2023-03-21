using System.ComponentModel.DataAnnotations;

namespace Employees.API.DTOs
{
    public class DayMonthYearDto
    {
        [Required(ErrorMessage = "Day is required.")]
        [Range(1, 31)]
        public int Day { get; set; }
        [Required(ErrorMessage = "Month is required.")]
        [Range(1, 12)]
        public int Month { get; set; }
        [Required(ErrorMessage = "Day is required.")]
        [Range(1900, int.MaxValue)]
        public int Year { get; set; }

    }
}
