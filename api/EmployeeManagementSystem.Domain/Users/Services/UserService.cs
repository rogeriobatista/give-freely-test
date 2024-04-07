using AutoMapper;
using EmployeeManagementSystem.Domain.Users.Dtos;
using EmployeeManagementSystem.Domain.Users.Entities;
using EmployeeManagementSystem.Domain.Users.Interfaces;
using EmployeeManagementSystem.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementSystem.Domain.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<UserResponseDto>> GetAsync()
        {
            return _mapper.Map<List<UserResponseDto>>(await _repository.ToListAsync());
        }

        public async Task<UserResponseDto> GetAsync(int id)
        {
            return _mapper.Map<UserResponseDto>(await _repository.GetByIdAsync(id));
        }

        public async Task<UserSignInResponseDto> SignInAsync(UserSignInDto dto)
        {
            var entity = await _repository.SignInAsync(dto.Email, dto.Password.Sha256());

            if (entity != null)
            {
                return new UserSignInResponseDto
                {
                    IsLogged = true,
                    AuthToken = GenerateToken(entity),
                    User = _mapper.Map<UserResponseDto>(entity)
                };
            }

            return new UserSignInResponseDto
            {
                IsLogged = false
            };
        }

        public async Task<UserResponseDto> CreateAsync(UserDto dto)
        {
            var user = new User(dto.Name, dto.Email, dto.Password);

            if (user.Validate())
            {
                return _mapper.Map<UserResponseDto>(await _repository.AddAsync(user));
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> ChangePasswordAsync(int id, string password)
        {
            var user = await _repository.GetByIdAsync(id);

            user.Password = password.Sha256();
            user.UpdatedAt = DateTime.Now;

            if (user.Validate())
            {
                _repository.Update(user);
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> UpdateAsync(int id, UserDto dto)
        {
            var user = await _repository.GetByIdAsync(id);

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.UpdatedAt = DateTime.Now;

            if (user.Validate())
            {
                _repository.Update(user);
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            _repository.Remove(user);
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Secret").Value);

            var claims = new Dictionary<string, object>
            {
                { "Id", user.Id },
                { "Name", user.Name },
                { "Email", user.Email },
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)

                }),
                Expires = DateTime.UtcNow.AddDays(99),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
