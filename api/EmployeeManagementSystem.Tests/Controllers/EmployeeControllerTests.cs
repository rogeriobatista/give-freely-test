using AutoFixture;
using EmployeeManagementSystem.Api.Controllers;
using EmployeeManagementSystem.Domain.Employees.Dtos;
using EmployeeManagementSystem.Domain.Employees.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace EmployeeManagementSystem.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private EmployeeController _controller;
        private readonly Fixture _fixture;

        public EmployeeControllerTests()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_employeeServiceMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllEmployeesAndShouldReturnOk()
        {
            var empployeeResponseDto = _fixture.CreateMany<EmployeeResponseDto>(10).ToList();

            _employeeServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(empployeeResponseDto);

            var response = await _controller.Get();

            var okResponse = response as OkObjectResult;

            var okResponseValue = okResponse?.Value as List<EmployeeResponseDto>;

            okResponse.Should().NotBeNull();
            okResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResponseValue.Any().Should().BeTrue();
            okResponseValue.Count.Should().Be(empployeeResponseDto.Count);

            _employeeServiceMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetOneEmployeeByIdAndShouldReturnOk()
        {
            var empployeeResponseDto = _fixture.Create<EmployeeResponseDto>();

            _employeeServiceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(empployeeResponseDto);

            var response = await _controller.Get(1);

            var okResponse = response as OkObjectResult;

            var okResponseValue = okResponse?.Value as EmployeeResponseDto;

            okResponse.Should().NotBeNull();
            okResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResponseValue.Should().NotBeNull();

            _employeeServiceMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCreateAnEmployeeAndReturnCreated()
        {
            var employeeDto = _fixture.Create<EmployeeDto>();
            var employeeResponseDto = _fixture.Create<EmployeeResponseDto>();

            employeeResponseDto.Errors = [];

            _employeeServiceMock.Setup(x => x.CreateAsync(employeeDto)).ReturnsAsync(employeeResponseDto);

            var response = await _controller.Create(employeeDto);

            var createdResult = response as CreatedResult;

            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be((int)HttpStatusCode.Created);

            _employeeServiceMock.Verify(x => x.CreateAsync(employeeDto), Times.Once);
        }

        [Fact]
        public async Task ShouldNotCreateAnEmployeeAndReturnBadRequest()
        {
            var employeeDto = _fixture.Create<EmployeeDto>();
            var employeeResponseDto = _fixture.Create<EmployeeResponseDto>();

            _employeeServiceMock.Setup(x => x.CreateAsync(employeeDto)).ReturnsAsync(employeeResponseDto);

            var response = await _controller.Create(employeeDto);

            var badRequestResult = response as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            _employeeServiceMock.Verify(x => x.CreateAsync(employeeDto), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateAnEmployeeAndReturnNoContent()
        {
            var employeeDto = _fixture.Create<EmployeeDto>();
            var employeeResponseDto = _fixture.Create<EmployeeResponseDto>();

            employeeResponseDto.Errors = [];

            _employeeServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), employeeDto)).ReturnsAsync(employeeResponseDto);

            var response = await _controller.Put(1, employeeDto);

            var noContentResult = response as NoContentResult;

            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

            _employeeServiceMock.Verify(x => x.UpdateAsync(It.IsAny<int>(), employeeDto), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateAnEmployeeAndReturnBadRequest()
        {
            var employeeDto = _fixture.Create<EmployeeDto>();
            var employeeResponseDto = _fixture.Create<EmployeeResponseDto>();

            _employeeServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), employeeDto)).ReturnsAsync(employeeResponseDto);

            var response = await _controller.Put(1, employeeDto);

            var badRequestResult = response as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            _employeeServiceMock.Verify(x => x.UpdateAsync(It.IsAny<int>(), employeeDto), Times.Once);
        }

        [Fact]
        public async Task ShouldDeleteAnEmployee()
        {
            await _controller.Delete(1);

            _employeeServiceMock.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
