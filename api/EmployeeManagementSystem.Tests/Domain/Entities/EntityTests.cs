using EmployeeManagementSystem.Domain.Employees.Entities;
using FluentAssertions;

namespace EmployeeManagementSystem.Tests.Domain.Entities
{
    public class EntityTests
    {
        [Fact]
        public void ShouldValidateEmployeeWithValidValues()
        {
            var employee = new Employee("firstName", "lastName", "email@email.com", "jobTitle", DateTime.Now.AddYears(-2));

            employee.Validate().Should().BeTrue();
        }

        [Fact]
        public void ShouldValidateEmployeeWithInvalidValues()
        {
            var employee = new Employee("firstName", "lastName", "email", "jobTitle", DateTime.Now);

            employee.Validate().Should().BeFalse();
            employee.ValidationResult.Should().NotBeNull();
            employee.ValidationResult.Errors.Count.Should().Be(2);
            employee.ValidationResult.Errors.Any(x => x.PropertyName == "Email" && x.ErrorMessage == "'Email' is not a valid email address.").Should().BeTrue();
            employee.ValidationResult.Errors.Any(x => x.PropertyName == "DateOfJoining.Date" && x.ErrorMessage == "'Date Of Joining Date' must be less than '09/04/2024 00:00:00'.").Should().BeTrue();
        }
    }
}
