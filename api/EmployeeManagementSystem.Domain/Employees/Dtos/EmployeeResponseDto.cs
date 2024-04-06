using EmployeeManagementSystem.Shared.Dtos;

namespace EmployeeManagementSystem.Domain.Employees.Dtos
{
    public class EmployeeResponseDto : BaseResponseDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int TotalOfYearsInTheCompany { get; set; }
    }
}
