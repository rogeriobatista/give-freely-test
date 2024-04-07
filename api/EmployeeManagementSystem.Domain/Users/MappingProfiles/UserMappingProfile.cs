using AutoMapper;
using EmployeeManagementSystem.Domain.Users.Dtos;
using EmployeeManagementSystem.Domain.Users.Entities;

namespace EmployeeManagementSystem.Domain.Users.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<User, UserResponseDto>()
                .ForMember(_ => _.Errors, x => x.MapFrom(_ => _.ValidationResult.Errors))
                .ReverseMap();

            CreateMap<UserResponseDto, UserDto>()
                .ReverseMap();
        }
    }
}
