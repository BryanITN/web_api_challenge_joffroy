using System.Collections.Generic;
using System.Linq;
using web_api_challenge.Dtos.Users;
using web_api_challenge.Exceptions;
using web_api_challenge.Models;
using web_api_challenge.Repositories.Interfaces;
using web_api_challenge.Services.Interfaces;
using web_api_challenge.Tokens;
using web_api_challenge.Utilities;

namespace web_api_challenge.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CreateUser(DtoCreateUpdateUser newUser)
        {
            var result = _userRepository.GetByFilter(u => u.UserName.Equals(newUser.UserName));
            if (result.Count() > 0)
                throw new JoffroyException("El username se encuentra en uso");
            User user = new User();
            user.UserName = newUser.UserName;
            user.Password = Encrypt.GetSHA256(newUser.Password);
            _userRepository.Add(user);
            _userRepository.Save();
        }

        public void DeleteUser(int userId)
        {
            var user = ValidateUser(userId);
            _userRepository.Delete(user);
            _userRepository.Save();
        }

        public void EditUser(DtoCreateUpdateUser editUser, int userId)
        {
            var user = ValidateUser(userId);
            user.Password = Encrypt.GetSHA256(editUser.Password);
            user.UserName = editUser.UserName;
            _userRepository.Update(user);
            _userRepository.Save();
        }

        public List<DtoUserView> GetAllUsers()
        {
            var result = _userRepository.GetAll().ToList().Select(u => new DtoUserView
            {
                Id = u.id
                ,
                UserName = u.UserName
            });
            return result.ToList();

        }

        public DtoUserView GetUserById(int userId)
        {
            var user = ValidateUser(userId);

            var result = new DtoUserView();
            result.UserName = user.UserName;
            result.Id = user.id;
            return result;
        }

        public DtoSingInResponse SingIn(DtoSingIn singInRequest)
        {
            singInRequest.Password = Encrypt.GetSHA256(singInRequest.Password);
            var user = _userRepository.GetByFilter(u => u.Password.Equals(singInRequest.Password) && u.UserName.Equals(singInRequest.UserName)).FirstOrDefault();
            if (user == null)
                throw new JoffroyException("Usuario y/o contraseña invalidos.");
            DtoSingInResponse response = new DtoSingInResponse();
            response.Token = TokenGenerator.GenerateTokenJwt(singInRequest.UserName);
            response.UserId = user.id;
            return response;
        }

        public User ValidateUser(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new JoffroyException($"El usuario con id: {userId} no existe.");
            return user;
        }

    }
}
