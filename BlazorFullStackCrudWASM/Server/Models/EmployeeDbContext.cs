using BlazorFullStackCrudWASM.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorFullStackCrudWASM.Server.Models
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
