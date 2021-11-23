using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chimera_v2.DTOs;

using Chimera_v2.Repository.Clients;


namespace Chimera_v2.Business.Implementations
{
    public class ClientBusinessImplementations : IClientBusiness
    {
        private readonly IClientRepository _repository;

        public ClientBusinessImplementations(IClientRepository repository)
        {
            _repository = repository;
        }

        public List<ClientDTO> GetAllClients()
        {
            try
            {
                return _repository.GetAllClients();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ClientDTO GetClientById(Guid id)
        {
            try
            {
                return _repository.GetClientById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ClientDTO CreateClient(ClientDTO clientDto)
        {
            try
            {
                return _repository.CreateClient(clientDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ClientDTO UpdateClient(ClientDTO clientDto)
        {
            try
            {
                return _repository.UpdateClient(clientDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                _repository.DeleteClient(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}