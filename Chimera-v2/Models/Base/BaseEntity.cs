using System;
using System.ComponentModel.DataAnnotations;

namespace Chimera_v2.Models.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
    }
}