using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class Incentive
    {
        [Key]
        public int Id { get; set; }
        public int IncentiveAmount { get; set; }
    }
}