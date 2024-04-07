using EmployeeManagementSystem.Shared.Dtos;

namespace EmployeeManagementSystem.Domain.Users.Dtos
{
    public class UserResponseDto : BaseResponseDto<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
