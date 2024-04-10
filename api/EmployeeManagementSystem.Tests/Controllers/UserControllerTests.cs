using AutoFixture;
using EmployeeManagementSystem.Api.Controllers;
using EmployeeManagementSystem.Domain.Employees.Dtos;
using EmployeeManagementSystem.Domain.Users.Dtos;
using EmployeeManagementSystem.Domain.Users.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace EmployeeManagementSystem.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private UserController _controller;
        private readonly Fixture _fixture;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllUsersAndShouldReturnOk()
        {
            var usersResponseDto = _fixture.CreateMany<UserResponseDto>(10).ToList();

            _userServiceMock.Setup(x => x.GetAsync()).ReturnsAsync(usersResponseDto);

            var response = await _controller.Get();

            var okResponse = response as OkObjectResult;

            var okResponseValue = okResponse?.Value as List<UserResponseDto>;

            okResponse.Should().NotBeNull();
            okResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResponseValue.Any().Should().BeTrue();
            okResponseValue.Count.Should().Be(usersResponseDto.Count);

            _userServiceMock.Verify(x => x.GetAsync(), Times.Once);
        }

        [Fact]
        public async Task GetOneUserByIdAndShouldReturnOk()
        {
            var userResponseDto = _fixture.Create<UserResponseDto>();

            _userServiceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(userResponseDto);

            var response = await _controller.Get(1);

            var okResponse = response as OkObjectResult;

            var okResponseValue = okResponse?.Value as UserResponseDto;

            okResponse.Should().NotBeNull();
            okResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResponseValue.Should().NotBeNull();

            _userServiceMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task ShouldSignInAnUserAndReturnOk()
        {
            var userSignInDto = _fixture.Create<UserSignInDto>();
            var userSignInResponseDto = _fixture.Create<UserSignInResponseDto>();

            _userServiceMock.Setup(x => x.SignInAsync(userSignInDto)).ReturnsAsync(userSignInResponseDto);

            var response = await _controller.SignIn(userSignInDto);

            var okResult = response as OkObjectResult;

            var userSignInResponse = okResult?.Value as UserSignInResponseDto;

            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            userSignInResponse.AuthToken.Should().NotBeNull();
            userSignInResponse.User.Should().NotBeNull();

            _userServiceMock.Verify(x => x.SignInAsync(userSignInDto), Times.Once);
        }

        [Fact]
        public async Task ShouldCreateAnUserAndReturnCreated()
        {
            var userDto = _fixture.Create<UserDto>();
            var userResponseDto = _fixture.Create<UserResponseDto>();

            userResponseDto.Errors = [];

            _userServiceMock.Setup(x => x.CreateAsync(userDto)).ReturnsAsync(userResponseDto);

            var response = await _controller.SignUp(userDto);

            var createdResult = response as CreatedResult;

            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be((int)HttpStatusCode.Created);

            _userServiceMock.Verify(x => x.CreateAsync(userDto), Times.Once);
        }

        [Fact]
        public async Task ShouldNotCreateAnUserAndReturnBadRequest()
        {
            var userDto = _fixture.Create<UserDto>();
            var userResponseDto = _fixture.Create<UserResponseDto>();

            _userServiceMock.Setup(x => x.CreateAsync(userDto)).ReturnsAsync(userResponseDto);

            var response = await _controller.SignUp(userDto);

            var badRequestResult = response as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            _userServiceMock.Verify(x => x.CreateAsync(userDto), Times.Once);
        }

        [Fact]
        public async Task ShouldChangeUserPasswordAndReturnNoContent()
        {
            var userResponseDto = _fixture.Create<UserResponseDto>();

            userResponseDto.Errors = [];

            _userServiceMock.Setup(x => x.ChangePasswordAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(userResponseDto);

            var response = await _controller.ChangePassword(1, "passwordUpdated");

            var noContentResult = response as NoContentResult;

            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

            _userServiceMock.Verify(x => x.ChangePasswordAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateAnUserAndReturnNoContent()
        {
            var userDto = _fixture.Create<UserDto>();
            var userResponseDto = _fixture.Create<UserResponseDto>();

            userResponseDto.Errors = [];

            _userServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), userDto)).ReturnsAsync(userResponseDto);

            var response = await _controller.Put(1, userDto);

            var noContentResult = response as NoContentResult;

            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

            _userServiceMock.Verify(x => x.UpdateAsync(It.IsAny<int>(), userDto), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateAnUserAndReturnBadRequest()
        {
            var userDto = _fixture.Create<UserDto>();
            var userResponseDto = _fixture.Create<UserResponseDto>();

            _userServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), userDto)).ReturnsAsync(userResponseDto);

            var response = await _controller.Put(1, userDto);

            var badRequestResult = response as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            _userServiceMock.Verify(x => x.UpdateAsync(It.IsAny<int>(), userDto), Times.Once);
        }

        [Fact]
        public async Task ShouldDeleteAnEmployee()
        {
            await _controller.Delete(1);

            _userServiceMock.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
