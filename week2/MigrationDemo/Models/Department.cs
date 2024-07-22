using System.ComponentModel.DataAnnotations;

namespace MigrationDemo.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
    }
}