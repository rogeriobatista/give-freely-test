namespace EmployeeManagementSystem.Domain.Users.Dtos
{
    public class UserSignInResponseDto
    {
        public bool IsLogged { get; set; }
        public UserResponseDto User { get; set; }
        public string AuthToken { get; set; }
    }
}
