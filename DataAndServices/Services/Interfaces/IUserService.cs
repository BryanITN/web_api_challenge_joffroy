using System.Collections.Generic;
using web_api_challenge.Dtos.Users;

namespace web_api_challenge.Services.Interfaces
{
   public interface IUserService
    {
        DtoSingInResponse SingIn(DtoSingIn singInRequest);
        void CreateUser(DtoCreateUpdateUser newUser);
        void EditUser(DtoCreateUpdateUser editUser,int userId);
        void DeleteUser(int userId);
        List<DtoUserView> GetAllUsers();
        DtoUserView GetUserById(int userId);
    }
}
