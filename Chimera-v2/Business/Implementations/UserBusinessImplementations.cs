using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;
using Chimera_v2.Models;
using Chimera_v2.Repository.Users;

namespace Chimera_v2.Business.Implementations
{
    public class UserBusinessImplementations : IUserBusiness
    {
        private readonly IUserRepository _repository;

        public UserBusinessImplementations(IUserRepository repository)
        {
            _repository = repository;
        }

        public List<UserDTO> GetAllUsers()
        {
            try
            {
                return _repository.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserDTO GetUserById(Guid id)
        {
            try
            {
                return _repository.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUserByUserName(string userName)
        {
            try
            {
                return _repository.GetUserByUserName(userName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserLoginDto Login(UserLoginDto userLoginDto)
        {
            try
            {
                return _repository.Login(userLoginDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserLoginDto Register(UserLoginDto userLoginDto)
        {
            try
            {
                return _repository.Register(userLoginDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserDTO UpdateUser(UserDTO userDTO)
        {
            try
            {
                return _repository.UpdateUser(userDTO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUser(Guid id)
        {
            try
            {
                _repository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UserExists(string userName)
        {
            try
            {
                return _repository.UserExists(userName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}