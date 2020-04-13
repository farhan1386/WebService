using System.Data.Entity;
using WebService.Models;

namespace WebService.Data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Incentive> Incentives { get; set; }
    }
}