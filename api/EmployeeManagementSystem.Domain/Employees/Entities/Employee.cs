using FluentValidation;
using EmployeeManagementSystem.Shared.Domain;

namespace EmployeeManagementSystem.Domain.Employees.Entities
{
    public class Employee : Entity<int, Employee>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public DateTime DateOfJoining { get; set; }

        protected Employee() { }

        public Employee(string firstName, string lastName, string email, string jobTitle, DateTime dateOfJoining)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            JobTitle = jobTitle;
            DateOfJoining = dateOfJoining;
        }

        public override bool Validate()
        {
            RuleFor(_ => _.FirstName)
                .NotEmpty();

            RuleFor(_ => _.LastName)
                .NotEmpty();

            RuleFor(_ => _.Email)
                .NotEmpty();

            RuleFor(_ => _.JobTitle)
                .NotEmpty();

            RuleFor(_ => _.DateOfJoining)
                .NotEmpty();

            RuleFor(_ => _.DateOfJoining.Date)
                .LessThan(DateTime.Now.Date);

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
