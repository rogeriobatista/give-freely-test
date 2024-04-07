using EmployeeManagementSystem.Domain.Users.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeManagementSystem.Domain.Users.Interfaces
{
    public interface IAuthService
    {
        UserResponseDto GetCurrentUser();
        void SetCurrentUserFromAuthToken(JwtSecurityToken authToken);
    }
}
