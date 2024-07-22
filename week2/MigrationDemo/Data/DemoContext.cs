using Microsoft.EntityFrameworkCore;
using MigrationDemo.Models;

namespace MigrationDemo.Data
{

    public class DemoContext : DbContext
    {

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DemoContext(DbContextOptions<DemoContext> options) : base(options) { }


    }

}