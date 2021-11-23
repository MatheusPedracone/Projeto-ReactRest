using System;
using Chimera_v2.Business;
using Chimera_v2.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chimera_v2.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;

        public ClientController(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

    // Busca todos os clients
        [HttpGet("GetClients")]
    // [Authorize(Roles = "Admin")]
        public ActionResult Get()
        {
            var clients = _clientBusiness.GetAllClients();
            if (clients == null)
            {
                return NotFound(new { erro = "Nenhum cliente encontrado!" });
            }
            return Ok(clients);
        }

    // Busca um client pelo id
        [HttpGet("GetClient/{id}")]
    // [Authorize(Roles = "Admin")]
        public ActionResult Get(Guid id)
        {
            var client = _clientBusiness.GetClientById(id);
            if (client == null)
            {
                return NotFound(new { erro = "Cliente não encontrado!" });
            }
            return Ok(client);
        }

    //Criação de um novo client
        [HttpPost("CreateClient")]
     // [Authorize]
        public ActionResult Post([FromBody] ClientDTO clientDto)
        {
            try
            {
                if (clientDto == null) return BadRequest();
                var newClientDto = _clientBusiness.CreateClient(clientDto);
                return Ok(newClientDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar criar cliente!. Erro: {ex.Message}");
            }
        }

    // Atualização de um client
        [HttpPut("UpdateClient")]
    // [Authorize]
        public ActionResult Put([FromBody] ClientDTO clientDto)
        {
            try
            {
                if (clientDto.Adress.Guid == null) return BadRequest();

                var client = _clientBusiness.UpdateClient(clientDto);
                return Ok(client);
            }
             catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar atualizar cliente!. Erro: {ex.Message}");
            }
        }

    //Deleção de um client
        [HttpDelete("DeleteClient/{id}")]
    // [Authorize]
        public ActionResult Delete(Guid id)
        {
            _clientBusiness.Delete(id);
            return NoContent();
        }
    }
}