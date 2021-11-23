using System;
using System.Collections.Generic;
using System.Linq;
using Chimera_v2.Data;
using Chimera_v2.DTOs;
using Chimera_v2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chimera_v2.Repository.Clients
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }
        public Client GetByNameTracking(string name)
        {
            return _context.Clients
                .FirstOrDefault(c => c.Name == name);
        }
        public Client GetByIdTracking(Guid id)
        {
            return _context.Clients
                .FirstOrDefault(c => c.Id == id);
        }
        public ClientDTO GetClientById(Guid id)
        {
            var client = _context.Clients
               .Include(c => c.Adress)
               .Where(c => c.Id.Equals(id))
               .Select(c => new ClientDTO
               {
                   Name = c.Name,
                   CPF = c.CPF,
                   IE = c.IE,
                   ContributorType = c.ContributorType,
                   Email = c.Email,
                   Phone = c.Phone,
                   Enabled = c.Enabled,
                   Adress = new AdressDTO
                   {
                       ZipCode = c.Adress.ZipCode,
                       Street = c.Adress.Street,
                       District = c.Adress.District,
                       County = c.Adress.County,
                       AdressNumber = c.Adress.AdressNumber,
                       UF = c.Adress.UF
                   }
               })
               .FirstOrDefault();
            return client ?? null;
        }
        public List<ClientDTO> GetAllClients()
        {
            return _context.Clients
              .Include(c => c.Adress)
              .Select(c => new ClientDTO
              {
                  Name = c.Name,
                  CPF = c.CPF,
                  IE = c.IE,
                  ContributorType = c.ContributorType,
                  Email = c.Email,
                  Phone = c.Phone,
                  Enabled = c.Enabled,
                  Adress = new AdressDTO
                  {
                      ZipCode = c.Adress.ZipCode,
                      Street = c.Adress.Street,
                      District = c.Adress.District,
                      County = c.Adress.County,
                      AdressNumber = c.Adress.AdressNumber,
                      UF = c.Adress.UF
                  }
              })
              .ToList();
        }
        public ClientDTO CreateClient(ClientDTO clientDto)
        {
            // crio a variavel que vai buscar o client pelo Name
            var client = GetByNameTracking(clientDto.Name);

            // vejo se esse client já existe
            if (client != default)
            {
               throw new Exception("Cliente já existe!");
            }
            // crio o novo client convertendo pra Dto
            _context.Clients.Add(new Client
            {
                Name = clientDto.Name,
                CPF = clientDto.CPF,
                IE = clientDto.IE,
                ContributorType = clientDto.ContributorType,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                Enabled = clientDto.Enabled,
                Adress = new Adress
                {
                    ZipCode = clientDto.Adress.ZipCode,
                    Street = clientDto.Adress.Street,
                    District = clientDto.Adress.District,
                    County = clientDto.Adress.County,
                    AdressNumber = clientDto.Adress.AdressNumber,
                    UF = clientDto.Adress.UF
                }
            });
            // salvo o novo client
            _context.SaveChanges();

            // retorno o novo client que foi criado
            return clientDto ?? null;
        }
        public ClientDTO UpdateClient(ClientDTO clientDto)
        {
            // aqui eu declarei o Client do banco, e dentro dele eu busco por Id e incluo o Adress
            var clientOrigin = _context.Clients
            .Where(c => c.Id == clientDto.Guid)
            .Include(c => c.Adress)
            .SingleOrDefault();

            // se o client que eu busquei por Id for dirente de null, eu vou fazer o update 
            if (clientOrigin != null)
            {
                // realizo o update
                _context.Entry(clientOrigin).CurrentValues.SetValues(clientDto);


                // qui eu declarei o Adress do banco, e dentro dele eu busco por guid e incluo o client
                var adressOrigin = _context.Adresses
                    .Where(c => c.Id == clientDto.Adress.Guid)
                    .Include(c => c.Client)
                    .SingleOrDefault();

                //se o guid for diferente de null, eu vou fazer o update
                if (clientDto.Adress.Guid != default)
                {
                    // Update adress
                    _context.Entry(adressOrigin).CurrentValues.SetValues(clientDto.Adress);
                }
                else
                {   // add adress
                    var newAdress = new AdressDTO
                    {
                        ZipCode = adressOrigin.ZipCode,
                        Street = adressOrigin.Street,
                        District = adressOrigin.District,
                        County = adressOrigin.County,
                        AdressNumber = adressOrigin.AdressNumber,
                        UF = adressOrigin.UF
                    };
                    _context.Adresses.Add(adressOrigin);
                }
            }
            _context.SaveChanges();
            return new ClientDTO
            {
                Name = clientDto.Name,
                CPF = clientDto.CPF,
                IE = clientDto.IE,
                ContributorType = clientDto.ContributorType,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                Enabled = clientDto.Enabled,
                Adress = new AdressDTO
                {
                    ZipCode = clientDto.Adress.ZipCode,
                    Street = clientDto.Adress.Street,
                    District = clientDto.Adress.District,
                    County = clientDto.Adress.County,
                    AdressNumber = clientDto.Adress.AdressNumber,
                    UF = clientDto.Adress.UF
                }
            };
        }
        public void DeleteClient(Guid id)
        {
            var client = _context.Clients
            .Where(c => c.Id == id)
            .Include(c => c.Adress).First();

            _context.Remove(client);
            _context.SaveChanges();
        }
    }
}