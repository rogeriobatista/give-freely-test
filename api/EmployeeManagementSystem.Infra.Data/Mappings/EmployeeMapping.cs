using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeeManagementSystem.Domain.Employees.Entities;
using EmployeeManagementSystem.Shared.Configurations;
using EmployeeManagementSystem.Shared.Resources;

namespace EmployeeManagementSystem.Infra.Data.Mappings
{
    public class EmployeeMapping : EntityTypeConfiguration<Employee>
    {
        public override void Map(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(_ => _.FirstName).IsRequired();

            builder.Property(_ => _.CreatedAt).IsRequired();

            builder.Property(_ => _.UpdatedAt).IsRequired();

            builder.Ignore(_ => _.CascadeMode);
            builder.Ignore(_ => _.ClassLevelCascadeMode);
            builder.Ignore(_ => _.RuleLevelCascadeMode);
            builder.Ignore(_ => _.ValidationResult);

            builder.ToTable(StringResource.EmployeeTableName);
        }
    }
}