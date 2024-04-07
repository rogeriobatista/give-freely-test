using EmployeeManagementSystem.Domain.Users.Entities;
using EmployeeManagementSystem.Shared.Configurations;
using EmployeeManagementSystem.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Infra.Data.Mappings
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.Property(_ => _.Name).IsRequired();

            builder.Property(_ => _.Email).IsRequired();

            builder.Property(_ => _.Password).IsRequired();

            builder.Property(_ => _.CreatedAt).IsRequired();

            builder.Property(_ => _.UpdatedAt).IsRequired();

            builder.Ignore(_ => _.CascadeMode);
            builder.Ignore(_ => _.ClassLevelCascadeMode);
            builder.Ignore(_ => _.RuleLevelCascadeMode);
            builder.Ignore(_ => _.ValidationResult);

            builder.HasData(User.Seed());

            builder.ToTable(StringResource.UserTableName);
        }
    }
}