using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;

namespace Chimera_v2.Business
{
    public interface IClientBusiness
    {
        List<ClientDTO> GetAllClients();
        ClientDTO GetClientById(Guid id);
        ClientDTO CreateClient(ClientDTO clientDto);
        ClientDTO UpdateClient(ClientDTO clientDto);
        void Delete(Guid id);
    }
}