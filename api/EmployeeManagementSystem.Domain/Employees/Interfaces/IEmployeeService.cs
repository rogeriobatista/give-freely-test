using EmployeeManagementSystem.Domain.Employees.Dtos;

namespace EmployeeManagementSystem.Domain.Employees.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponseDto>> GetAsync(string? filter);
        Task<EmployeeResponseDto> GetAsync(int id);
        Task<EmployeeResponseDto> CreateAsync(EmployeeDto dto);
        Task<EmployeeResponseDto> UpdateAsync(int id, EmployeeDto dto);
        Task DeleteAsync(int id);
    }
}
