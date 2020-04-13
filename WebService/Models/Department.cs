using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string DepartmentName { get; set; }
    }
}