using AutoMapper;
using URIS_Grupa11.DTO;
using URIS_Grupa11.Models;

namespace URIS_Grupa11.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Morao si reci sta se u sta mapira zato sto koristis AutoMapper
            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>(); //dodato zbog Create
        }
    }
}
