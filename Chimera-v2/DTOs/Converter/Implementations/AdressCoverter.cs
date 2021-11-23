using System.Collections.Generic;
using System.Linq;
using Chimera_v2.DTOs.Converter.Contract;
using Chimera_v2.Models;

namespace Chimera_v2.DTOs.Converter.Implementations
{
    public class AdressCoverter : IParser<AdressDTO, Adress>, IParser<Adress, AdressDTO>
    {
        public AdressDTO Parse(Adress origin)
        {
            if (origin == null) return null;
            return new AdressDTO
            {
                ZipCode = origin.ZipCode,
                Street = origin.Street,
                District = origin.District,
                County = origin.County,
                AdressNumber = origin.AdressNumber,
                UF = origin.UF
            };
        }
        public Adress Parse(AdressDTO origin)
        {
            if (origin == null) return null;
            return new Adress
            {
                ZipCode = origin.ZipCode,
                Street = origin.Street,
                District = origin.District,
                County = origin.County,
                AdressNumber = origin.AdressNumber,
                UF = origin.UF
            };
        }
        public List<AdressDTO> Parse(List<Adress> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        public List<Adress> Parse(List<AdressDTO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}