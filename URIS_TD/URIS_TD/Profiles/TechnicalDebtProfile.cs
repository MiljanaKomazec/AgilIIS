using AutoMapper;
using URIS_TD.DTO;
using URIS_TD.Models;

namespace URIS_TD.Profiles
{
    public class TechnicalDebtProfile : Profile
    {

        public TechnicalDebtProfile()
        {
            CreateMap<TechnicalDebt, TechnicalDebtDto> ().ReverseMap();
        }
    }
}
