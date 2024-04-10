using AutoFixture;
using AutoMapper;
using EmployeeManagementSystem.Domain.Users.Dtos;
using EmployeeManagementSystem.Domain.Users.Entities;
using EmployeeManagementSystem.Domain.Users.Interfaces;
using EmployeeManagementSystem.Domain.Users.Services;
using EmployeeManagementSystem.Infra.IoC.Configurations;
using EmployeeManagementSystem.Shared.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace EmployeeManagementSystem.Tests.Domain.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly Fixture _fixture;

        public UserServiceTests()
        {
            var myConfiguration = new Dictionary<string, string>
            { 
                {"Secret", "fedaf7d8863b48e197b9287d492b708e"},
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            _userRepositoryMock = new Mock<IUserRepository>();
            _mapper = ConfigureDependencies.CreateMapper();
            _userService = new UserService(_userRepositoryMock.Object, _mapper, _configuration);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task ShouldListUsers()
        {
            var users = _fixture.CreateMany<User>().ToList();

            _userRepositoryMock.Setup(x => x.ToListAsync()).ReturnsAsync(users);

            var response = await _userService.GetAsync();

            response.Should().NotBeNull();
            response.Any().Should().BeTrue();
            response.Count().Should().Be(users.Count);

            _userRepositoryMock.Verify(x => x.ToListAsync(), Times.Once);
        }

        [Fact]
        public async Task ShouldGetUserById()
        {
            var user = _fixture.Create<User>();

            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            var response = await _userService.GetAsync(1);

            response.Should().NotBeNull();

            _userRepositoryMock.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task ShouldSignInUser()
        {
            var user = new User("name", "email@email.test.com", "password");
            var userSignInDto = new UserSignInDto { Email = "email@email.test.com", Password = "password" };

            _userRepositoryMock.Setup(x => x.SignInAsync(userSignInDto.Email, userSignInDto.Password.Sha256())).ReturnsAsync(user);

            var response = await _userService.SignInAsync(userSignInDto);

            response.Should().NotBeNull();
            response.AuthToken.Should().NotBeNull();
            response.User.Should().NotBeNull();
            response.User.Name.Should().Be(user.Name);
            response.User.Email.Should().Be(user.Email);

            _userRepositoryMock.Verify(x => x.SignInAsync(userSignInDto.Email, userSignInDto.Password.Sha256()), Times.Once);
        }

        [Fact]
        public async Task ShouldCreateUser()
        {
            var user = new User("name", "email@email.test.com", "password");
            var userDto = new UserDto { Name = "name", Email = "email@email.test.com", Password = "password" };

            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

            var response = await _userService.CreateAsync(userDto);

            response.Should().NotBeNull();
            response.Name.Should().Be(userDto.Name);
            response.Email.Should().Be(userDto.Email);

            _userRepositoryMock.Verify(x => x.AddAsync(user), Times.Once);
        }

        [Fact]
        public async Task ShouldChangeUserPassword()
        {
            var user = new User("name", "email@email.test.com", "password");

            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            var response = await _userService.ChangePasswordAsync(1, "passwordUpdated");

            response.Should().NotBeNull();

            _userRepositoryMock.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateUser()
        {
            var user = new User("name", "email@email.test.com", "password");
            var userDto = new UserDto { Name = "nameUpdated", Email = "email.updated@email.test.com" };

            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            var response = await _userService.UpdateAsync(1, userDto);

            response.Should().NotBeNull();
            response.Name.Should().Be(userDto.Name);
            response.Email.Should().Be(userDto.Email);

            _userRepositoryMock.Verify(x => x.GetByIdAsync(1), Times.Once);
            _userRepositoryMock.Verify(x => x.Update(user), Times.Once);
        }

        [Fact]
        public async Task ShouldDeleteEmployee()
        {
            var user = new User("name", "email@email.test.com", "password");

            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            await _userService.DeleteAsync(1);

            _userRepositoryMock.Verify(x => x.GetByIdAsync(1), Times.Once);
            _userRepositoryMock.Verify(x => x.Remove(user), Times.Once);
        }
    }
}
