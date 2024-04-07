using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Domain.Employees.Entities;
using EmployeeManagementSystem.Domain.Employees.Interfaces;
using EmployeeManagementSystem.Infra.Data.Contexts;
using EmployeeManagementSystem.Shared.Repository;
using EmployeeManagementSystem.Domain.Users.Interfaces;
using EmployeeManagementSystem.Domain.Users.Entities;

namespace EmployeeManagementSystem.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<int, User>, IUserRepository
    {
        private readonly EmployeeManagementSystemContext _context;

        public UserRepository(EmployeeManagementSystemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> SignInAsync(string email, string password) =>
            await _context.Set<User>().FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
}
