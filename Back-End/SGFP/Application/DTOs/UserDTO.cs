namespace SGFP.Application.DTOs
{
    public class UserDTO
    {
        public UserDTO(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }

    }
}
