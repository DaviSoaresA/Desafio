using SGFP.Application.DTOs;
using SGFP.Domain.Entities;
using SGFP.Domain.Interfaces;
using SGFP.Infrastructure.Repositories;

namespace SGFP.Domain.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var userGet = await _repository.GetByIdAsync(id);
            if (userGet == null)
            {
                throw new Exception("Usuário não encontrado!");
            }
            User user = userGet; 
            return new UserDTO(user.Name, user.Email);
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            if (await _repository.GetByEmailAsync(email) == null)
            {
                throw new Exception("Usuário não encontrado!");
            }
            User user = await _repository.GetByEmailAsync(email);
            return new UserDTO(user.Name, user.Email);
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _repository.GetByEmailAsync(email);

            if (user == null)
            {
                return null;
            }
            if (!user.Password.Equals(password))
            {
                return null;
            }

            return user;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            List<UserDTO> userDTOs = new();
            foreach (var user in await _repository.GetAllAsync())
            {
                userDTOs.Add(new UserDTO(user.Name, user.Email));
            }
            return userDTOs;
        }

        public async Task AddAsync(UserInsertDTO insertDTO)
        {
            if (!insertDTO.ConfirmPassword.Equals(insertDTO.Password))
            {
                throw new Exception("As senhas estão diferentes");
            }
            if (_repository.GetByEmailAsync(insertDTO.Email).Result != null)
            {
                throw new Exception("Email já existente");
            }

            User user = new User(insertDTO.Name, insertDTO.Email, insertDTO.Password);
            await _repository.AddAsync(user);
        }

        public async Task UpdateAsync(Guid id,UserDTO userDTO)
        {
            if (await _repository.GetByIdAsync(id) == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado ou inválido!");
            }
            if (_repository.GetByEmailAsync(userDTO.Email).Result != null)
            {
                throw new Exception("Email já existente");
            }
            User user = await _repository.GetByIdAsync(id);

            await _repository.UpdateAsync(new User(user.Id, userDTO.Name, userDTO.Email, user.Password, user.Finances));
        }

        public async Task DeleteAsync(Guid id)
        {
            if (await _repository.GetByIdAsync(id) == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado ou inválido!");
            }
            await _repository.DeleteAsync(id);
        }
    }
}
