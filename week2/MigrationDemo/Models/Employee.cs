using System.ComponentModel.DataAnnotations;

namespace MigrationDemo.Models
{
    public class Employee
    {
        [Key]
        public int EmployeesID { get; set; }
        [Required]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}