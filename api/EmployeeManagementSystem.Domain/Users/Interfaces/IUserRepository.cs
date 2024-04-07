using EmployeeManagementSystem.Domain.Users.Entities;
using EmployeeManagementSystem.Shared.Repository;

namespace EmployeeManagementSystem.Domain.Users.Interfaces
{
    public interface IUserRepository : IBaseRepository<int, User>
    {
        Task<User?> SignInAsync(string email, string password);
    }
}
