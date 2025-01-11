namespace SGFP.Application.DTOs
{
    public class UserInsertDTO
    {
        public UserInsertDTO(string name, string email, string password, string confirmPassword)
        {
            Name = name;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }
    }
}
