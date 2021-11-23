using System.ComponentModel.DataAnnotations;
using Chimera_v2.Models.Base;

namespace Chimera_v2.Models
{
    public class Client : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string CPF { get; set; }

        [Required]
        public string IE { get; set; }

        [Required]
        public string ContributorType { get; set; }
        
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Required]
        public bool Enabled { get; set; }
        public Adress Adress { get; set; }
    }
}