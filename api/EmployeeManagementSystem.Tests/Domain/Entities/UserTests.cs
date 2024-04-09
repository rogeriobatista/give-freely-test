using EmployeeManagementSystem.Domain.Employees.Entities;
using EmployeeManagementSystem.Domain.Users.Entities;
using FluentAssertions;

namespace EmployeeManagementSystem.Tests.Domain.Entities
{
    public class UserTests
    {
        [Fact]
        public void ShouldValidateUserWithValidValues()
        {
            var user = new User("firstName", "email@email.com", "password");

            user.Validate().Should().BeTrue();
        }

        [Fact]
        public void ShouldValidateUserWithInvalidValues()
        {
            var user = new User("firstName", "email", "password");

            user.Validate().Should().BeFalse();
            user.ValidationResult.Should().NotBeNull();
            user.ValidationResult.Errors.Count.Should().Be(1);
            user.ValidationResult.Errors.Any(x => x.PropertyName == "Email" && x.ErrorMessage == "'Email' is not a valid email address.").Should().BeTrue();
        }
    }
}
