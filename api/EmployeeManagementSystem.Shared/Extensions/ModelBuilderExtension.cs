using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Shared.Configurations;

namespace EmployeeManagementSystem.Shared.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityTypeConfiguration<TEntity> configuration) where TEntity : class
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }
    }
}
