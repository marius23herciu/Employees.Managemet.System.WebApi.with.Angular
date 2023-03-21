using System.ComponentModel.DataAnnotations;

namespace Employees.API.DTOs
{
    public class AddressDto
    {
        [Required(ErrorMessage = "City name is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Street name is required.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Number is required.")]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }
    }
}
