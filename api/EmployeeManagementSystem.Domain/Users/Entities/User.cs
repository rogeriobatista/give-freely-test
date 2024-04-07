using EmployeeManagementSystem.Shared.Domain;
using EmployeeManagementSystem.Shared.Extensions;
using FluentValidation;

namespace EmployeeManagementSystem.Domain.Users.Entities
{
    public class User : Entity<int, User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        protected User() { }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password.Sha256();
        }

        public override bool Validate()
        {
            RuleFor(_ => _.Name)
                .NotEmpty();

            RuleFor(_ => _.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(_ => _.Password)
                .NotEmpty();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public static User Seed()
        {
            return new User
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@ems.com",
                Password = "p@ssw0rd".Sha256()
            };
        }
    }
}
