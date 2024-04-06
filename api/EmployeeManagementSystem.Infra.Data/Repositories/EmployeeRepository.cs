using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Domain.Employees.Entities;
using EmployeeManagementSystem.Domain.Employees.Interfaces;
using EmployeeManagementSystem.Infra.Data.Contexts;
using EmployeeManagementSystem.Shared.Repository;

namespace EmployeeManagementSystem.Infra.Data.Repositories
{
    public class EmployeeRepository : BaseRepository<int, Employee>, IEmployeeRepository
    {
        private readonly EmployeeManagementSystemContext _context;

        public EmployeeRepository(EmployeeManagementSystemContext context) : base(context)
        {
            _context = context;
        }
    }
}
