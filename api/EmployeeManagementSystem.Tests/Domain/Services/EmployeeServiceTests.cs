using AutoFixture;
using AutoMapper;
using EmployeeManagementSystem.Domain.Employees.Dtos;
using EmployeeManagementSystem.Domain.Employees.Entities;
using EmployeeManagementSystem.Domain.Employees.Interfaces;
using EmployeeManagementSystem.Domain.Employees.Services;
using EmployeeManagementSystem.Infra.IoC.Configurations;
using FluentAssertions;
using Moq;

namespace EmployeeManagementSystem.Tests.Domain.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly IMapper _mapper;
        private readonly EmployeeService _employeeService;
        private readonly Fixture _fixture;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mapper = ConfigureDependencies.CreateMapper();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object, _mapper);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task ShouldListEmployees()
        {
            var employees = _fixture.CreateMany<Employee>().ToList();

            _employeeRepositoryMock.Setup(x => x.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Employee, bool>>>())).ReturnsAsync(employees);

            var response = await _employeeService.GetAsync("");

            response.Should().NotBeNull();
            response.Any().Should().BeTrue();
            response.Count().Should().Be(employees.Count);

            _employeeRepositoryMock.Verify(x => x.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Employee, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task ShouldGetEmployeeById()
        {
            var employee = _fixture.Create<Employee>();

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(employee);

            var response = await _employeeService.GetAsync(1);

            response.Should().NotBeNull();

            _employeeRepositoryMock.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task ShouldCreateEmployee()
        {
            var validDateOfJoining = DateTime.Now.AddYears(-2);
            var employee = new Employee("firstName", "lastName", "email@email.test.com", "jobTitle", validDateOfJoining);
            var employeeDto = new EmployeeDto { FirstName = "firstName", LastName = "lastName", Email = "email@email.test.com", JobTitle = "jobTitle", DateOfJoining = validDateOfJoining };

            _employeeRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Employee>())).ReturnsAsync(employee);

            var response = await _employeeService.CreateAsync(employeeDto);

            response.Should().NotBeNull();
            response.FirstName.Should().Be(employeeDto.FirstName);
            response.LastName.Should().Be(employeeDto.LastName);
            response.Email.Should().Be(employeeDto.Email);
            response.JobTitle.Should().Be(employeeDto.JobTitle);
            response.DateOfJoining.Should().Be(employeeDto.DateOfJoining);

            _employeeRepositoryMock.Verify(x => x.AddAsync(employee), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateEmployee()
        {
            var validDateOfJoining = DateTime.Now.AddYears(-2);
            var validDateOfJoiningUpdated = DateTime.Now.AddYears(-3);
            var employee = new Employee("firstName", "lastName", "email@email.test.com", "jobTitle", validDateOfJoining);
            var employeeDto = new EmployeeDto { FirstName = "firstNameUpdated", LastName = "lastNameUpdated", Email = "email.updated@email.test.com", JobTitle = "jobTitleUpdated", DateOfJoining = validDateOfJoiningUpdated };

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(employee);

            var response = await _employeeService.UpdateAsync(1, employeeDto);

            response.Should().NotBeNull();
            response.FirstName.Should().Be(employeeDto.FirstName);
            response.LastName.Should().Be(employeeDto.LastName);
            response.Email.Should().Be(employeeDto.Email);
            response.JobTitle.Should().Be(employeeDto.JobTitle);
            response.DateOfJoining.Should().Be(employeeDto.DateOfJoining);

            _employeeRepositoryMock.Verify(x => x.GetByIdAsync(1), Times.Once);
            _employeeRepositoryMock.Verify(x => x.Update(employee), Times.Once);
        }

        [Fact]
        public async Task ShouldDeleteEmployee()
        {
            var employee = new Employee("firstName", "lastName", "email@email.test.com", "jobTitle", DateTime.Now.AddYears(-2));

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(employee);

            await _employeeService.DeleteAsync(1);

            _employeeRepositoryMock.Verify(x => x.GetByIdAsync(1), Times.Once);
            _employeeRepositoryMock.Verify(x => x.Remove(employee), Times.Once);
        }
    }
}
