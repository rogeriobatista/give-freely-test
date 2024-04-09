using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManagementSystem.Domain.Employees.Interfaces;
using EmployeeManagementSystem.Domain.Employees.Services;
using EmployeeManagementSystem.Infra.Data.Contexts;
using EmployeeManagementSystem.Infra.Data.Repositories;
using EmployeeManagementSystem.Infra.Data.UnityOfWork;
using EmployeeManagementSystem.Domain.Users.Interfaces;
using EmployeeManagementSystem.Domain.Users.Services;
using AutoMapper;
using EmployeeManagementSystem.Domain.Employees.MappingProfiles;
using EmployeeManagementSystem.Domain.Users.MappingProfiles;

namespace EmployeeManagementSystem.Infra.IoC.Configurations
{
    public static class ConfigureDependencies
    {
        public static void AddContextDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeManagementSystemContext>(options => {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("EmployeeManagementSystem.Infra.Data"));
            });
        }

        public static void AddApplicationDependecies(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnitOfWork>();
            services.AddSingleton<IAuthService, AuthService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        public static IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EmployeeMappingProfile());
                mc.AddProfile(new UserMappingProfile());
            });

            return mapperConfig.CreateMapper();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(CreateMapper());
        }
    }
}
