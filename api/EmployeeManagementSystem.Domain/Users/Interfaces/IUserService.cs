using EmployeeManagementSystem.Domain.Users.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeManagementSystem.Domain.Users.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAsync();
        Task<UserResponseDto> GetAsync(int id);
        Task<UserSignInResponseDto> SignInAsync(UserSignInDto dto);
        Task<UserResponseDto> CreateAsync(UserDto dto);
        Task<UserResponseDto> ChangePasswordAsync(int id, string password);
        Task<UserResponseDto> UpdateAsync(int id, UserDto dto);
        Task DeleteAsync(int id);
    }
}
