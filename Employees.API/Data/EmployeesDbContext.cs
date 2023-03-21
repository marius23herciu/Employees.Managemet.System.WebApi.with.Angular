using Employees.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.API.Data
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<DayMonthYear> Dates { get; set; }

    }
}
