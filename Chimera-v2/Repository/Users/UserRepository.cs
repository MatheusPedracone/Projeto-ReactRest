using System;
using System.Collections.Generic;
using System.Linq;
using Chimera_v2.Data;
using Chimera_v2.DTOs;
using Chimera_v2.Models;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Chimera_v2.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public User GetByIdTracking(Guid id)
        {
            return _context.Users
                .FirstOrDefault(c => c.Id == id);
        }

        public List<UserDTO> GetAllUsers()
        {
            return _context.Users.Select(u => new UserDTO
            {
                Username = u.Username,
                Password = "",
                Role = u.Role
            }).ToList();
        }
        public UserDTO GetUserById(Guid id)
        {
            var user = _context.Users
            .AsNoTracking()
            .Where(u => u.Id.Equals(id))
            .Select(u => new UserDTO
            {
                Username = u.Username,
                Password = "",
                Role = u.Role
            })
            .FirstOrDefault();
            return user ?? null;
        }
        public User GetUserByUserName(string userName)
        {
            return _context.Users.AsNoTracking().Select(u => new User
            {
                Username = u.Username,
                Password = u.Password,
                Role = u.Role
            }).FirstOrDefault(u => u.Username == userName);
        }
        public UserLoginDto Login(UserLoginDto userLoginDto)
        {
            var userContext = GetUserByUserName(userLoginDto.Username);

            //verifico se user existe e se a senha é igual a do banco
            if (BC.Verify(userLoginDto.Password, userContext.Password))
            {
                return new UserLoginDto
                {
                    Username = userContext.Username,
                    Password = userContext.Password
                };
            }
            return null;
        }
        public UserDTO UpdateUser(UserDTO userDTO)
        {
            var userContext = GetByIdTracking(userDTO.Id);

            if (userContext != null)
            {
                userDTO.Password = BC.HashPassword(userDTO.Password);
                _context.Entry(userContext).CurrentValues.SetValues(userDTO);
                _context.SaveChanges();
            }
            return new UserDTO
            {
                Username = userDTO.Username,
                Password = BC.HashPassword(userDTO.Password),
                Role = "Usuário"
            };
        }
        public UserLoginDto Register(UserLoginDto userLoginDto)
        {
            _context.Users.Add(new User
            {
                Username = userLoginDto.Username,
                Password = BC.HashPassword(userLoginDto.Password),
                Role = "Usuário"
            });
            _context.SaveChanges();
            return new UserLoginDto
            {
                Username = userLoginDto.Username,
                Password = userLoginDto.Password
            };
        }

        public void DeleteUser(Guid id)
        {
            var UserToDelete = _context.Users.Where(u => u.Id == id).First();

            _context.Remove(UserToDelete);
            _context.SaveChanges();
        }
        public bool UserExists(string userName)
        {
            return _context.Users.Any(u => u.Username == userName.ToLower());
        }
    }
}