using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Infra.Data.Mappings;
using EmployeeManagementSystem.Shared.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeManagementSystem.Infra.Data.Contexts
{
    public class EmployeeManagementSystemContext : DbContext
    {
        public EmployeeManagementSystemContext(DbContextOptions<EmployeeManagementSystemContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddConfiguration(new EmployeeMapping());
        }
    }
}
