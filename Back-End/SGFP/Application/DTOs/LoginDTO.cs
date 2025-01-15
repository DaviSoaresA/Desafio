namespace SGFP.Application.DTOs
{
    public class LoginDTO
    {
        public LoginDTO(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
