using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;


namespace Chimera_v2.Repository.Clients
{
    public interface IClientRepository
    {
        ClientDTO GetClientById(Guid id);
        List<ClientDTO> GetAllClients();
        ClientDTO CreateClient(ClientDTO clientDto);
        ClientDTO UpdateClient(ClientDTO clientDto);
        void DeleteClient(Guid id);
    }
}