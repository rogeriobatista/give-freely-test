namespace EmployeeManagementSystem.Domain.Employees.Dtos
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}
