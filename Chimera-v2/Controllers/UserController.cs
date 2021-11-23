using System;
using System.Linq;
using System.Threading.Tasks;
using Chimera_v2.Data;
using Chimera_v2.DTOs;
using Chimera_v2.Models;
using Chimera_v2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chimera_v2.Business;
using Microsoft.AspNetCore.Http;

namespace Chimera_v2.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserBusiness _userBusiness;

        public UserController(ITokenService tokenService, IUserBusiness userBusiness)
        {
            _tokenService = tokenService;
            _userBusiness = userBusiness;
        }

        [HttpPost("Login")]
        public ActionResult<dynamic> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var userContext = _userBusiness.GetUserByUserName(userLoginDto.Username);
                if (userContext == null)
                {
                    return BadRequest(new { erro = "Usuario não existe!" });
                }
                var userToLogin = _userBusiness.Login(userLoginDto);
                var token = _tokenService.GenerateToken(userContext);
                userContext.Password = "";
                return Ok(new
                {
                    userContext = new UserDTO { Username = userContext.Username, Password = "", Role = userContext.Role },
                    token = token,
                    mesangem = "Autenticado com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar efetuar login!. Erro: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        public ActionResult<dynamic> Register([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                if (_userBusiness.UserExists(userLoginDto.Username))
                {
                    return BadRequest(new { Erro = "Usuário já existe!" });
                }
                else
                {
                    _userBusiness.Register(userLoginDto);
                }
                return new
                {
                    user = userLoginDto,
                    mensagem = "Usuário cadastrado com sucesso!"
                };
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar criar usuário!. Erro: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        // [Authorize(Roles = "Admin")]
        public ActionResult<dynamic> UpdateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                var userToUpdate = _userBusiness.UpdateUser(userDTO);
                if (userToUpdate == null) return BadRequest("Erro ao tentar adicionar evento.");
                return Ok(userToUpdate);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar atualizar usuário!. Erro: {ex.Message}");
            }
        }

        [HttpGet("GetAllUsers")]
        // [Authorize(Roles = "Admin")]
        public ActionResult<dynamic> GetAll()
        {
            try
            {
                var users = _userBusiness.GetAllUsers();
                if (users == null)
                    return NotFound(new { erro = "Não foi encontrado nenhum usuário!" });
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar buscar usuários!. Erro: {ex.Message}");
            }
        }

        [HttpGet("GetUser/{id}")]
        // [Authorize(Roles = "Admin")]
        public ActionResult<dynamic> Get(Guid id)
        {
            try
            {
                var user = _userBusiness.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new { erro = "Não foi encontrado nenhum usuário!" });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar buscar usuário!. Erro: {ex.Message}");
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        // [Authorize(Roles = "Admin")]
        public ActionResult<dynamic> Delete(Guid id)
        {
            try
            {
                _userBusiness.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar deletar usuário!. Erro: {ex.Message}");
            }
        }
    }
}