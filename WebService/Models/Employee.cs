using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public int Age { get; set; }

        [StringLength(100)]
        public string Office { get; set; }

        [StringLength(100)]
        public string Position { get; set; }

        public int Salary { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int IncentiveId { get; set; }
        public Incentive Incentive { get; set; }
    }
}