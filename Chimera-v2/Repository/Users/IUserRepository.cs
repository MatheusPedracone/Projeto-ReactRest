using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;
using Chimera_v2.Models;

namespace Chimera_v2.Repository.Users
{
    public interface IUserRepository
    {
        bool UserExists (string userName);
        List<UserDTO> GetAllUsers();
        UserDTO GetUserById(Guid id);
        User GetUserByUserName(string userName);
        UserLoginDto Register(UserLoginDto userLoginDto);
        UserLoginDto Login (UserLoginDto userLoginDto);
        UserDTO UpdateUser (UserDTO userDTO);
        void DeleteUser(Guid id);
    }
}