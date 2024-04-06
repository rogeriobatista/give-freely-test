using AutoMapper;
using EmployeeManagementSystem.Domain.Employees.Dtos;
using EmployeeManagementSystem.Domain.Employees.Entities;

namespace EmployeeManagementSystem.Domain.Employees.MappingProfiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();

            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(_ => _.TotalOfYearsInTheCompany, x => x.MapFrom(_ => DateTime.Now.Year - _.DateOfJoining.Year))
                .ForMember(_ => _.Errors, x => x.MapFrom(_ => _.ValidationResult.Errors))
                .ReverseMap();

            CreateMap<EmployeeResponseDto, EmployeeDto>()
                .ReverseMap();
        }
    }
}
