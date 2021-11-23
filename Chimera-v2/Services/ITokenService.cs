using Chimera_v2.DTOs;
using Chimera_v2.Models;

namespace Chimera_v2.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}