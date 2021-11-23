using Chimera_v2.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Chimera_v2.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}