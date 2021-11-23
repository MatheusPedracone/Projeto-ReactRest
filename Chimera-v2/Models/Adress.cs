
using System;
using Chimera_v2.Models.Base;

namespace Chimera_v2.Models
{
    public class Adress : BaseEntity
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string County { get; set; }
        public string AdressNumber { get; set; }
        public string UF { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}

