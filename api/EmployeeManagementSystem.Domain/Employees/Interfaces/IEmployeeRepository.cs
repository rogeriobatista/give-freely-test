using EmployeeManagementSystem.Domain.Employees.Entities;
using EmployeeManagementSystem.Shared.Repository;

namespace EmployeeManagementSystem.Domain.Employees.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<int, Employee>
    {
    }
}
