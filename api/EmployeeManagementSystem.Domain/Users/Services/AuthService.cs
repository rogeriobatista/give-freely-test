using EmployeeManagementSystem.Domain.Users.Dtos;
using EmployeeManagementSystem.Domain.Users.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeManagementSystem.Domain.Users.Services
{
    public class AuthService : IAuthService
    {
        private UserResponseDto _currentUser { get; set; }

        public UserResponseDto GetCurrentUser()
        {
            return _currentUser;
        }

        public void SetCurrentUserFromAuthToken(JwtSecurityToken authToken)
        {
            if (authToken == null) return;

            _currentUser = new UserResponseDto
            {
                Id = Convert.ToInt32(authToken?.Claims.First(claim => claim.Type == "Id").Value),
                Name = authToken?.Claims.First(claim => claim.Type == "Name").Value ?? string.Empty,
                Email = authToken?.Claims.First(claim => claim.Type == "Email").Value ?? string.Empty
            };
        }
    }
}
