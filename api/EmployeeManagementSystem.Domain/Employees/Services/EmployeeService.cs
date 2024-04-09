using AutoMapper;
using EmployeeManagementSystem.Domain.Employees.Dtos;
using EmployeeManagementSystem.Domain.Employees.Entities;
using EmployeeManagementSystem.Domain.Employees.Interfaces;

namespace EmployeeManagementSystem.Domain.Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponseDto>> GetAsync(string? filter) =>
            _mapper.Map<List<EmployeeResponseDto>>(await _repository.GetAsync(x => string.IsNullOrEmpty(filter) || (x.FirstName.Contains(filter) || x.LastName.Contains(filter) || x.JobTitle.Contains(filter))));

        public async Task<EmployeeResponseDto> GetAsync(int id) =>
            _mapper.Map<EmployeeResponseDto>(await _repository.GetByIdAsync(id));

        public async Task<EmployeeResponseDto> CreateAsync(EmployeeDto dto)
        {
            var entity = new Employee(dto.FirstName, dto.LastName, dto.Email, dto.JobTitle, dto.DateOfJoining);

            if (entity.Validate())
            {
                return _mapper.Map<EmployeeResponseDto>(await _repository.AddAsync(entity));
            }

            return _mapper.Map<EmployeeResponseDto>(entity);
        }

        public async Task<EmployeeResponseDto> UpdateAsync(int id, EmployeeDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);

            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Email = dto.Email;
            entity.JobTitle = dto.JobTitle;
            entity.DateOfJoining = dto.DateOfJoining;
            entity.UpdatedAt = DateTime.Now;

            if (entity.Validate())
            {
                _repository.Update(entity);
            }

            return _mapper.Map<EmployeeResponseDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            _repository.Remove(entity);
        }
    }
}
