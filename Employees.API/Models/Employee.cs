namespace Employees.API.Models
{
    public class Employee
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DayMonthYear? DOB { get; set; }
        public string? Email { get; set; }
        public long Phone { get; set; }
        public Address? Address { get; set; }
        public long Salary { get; set; }
    }
}
