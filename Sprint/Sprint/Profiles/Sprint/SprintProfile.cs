using AutoMapper;
using Sprint.Models.ModelSprint;

namespace Sprint.Profiles.SprintProfile
{
    public class SprintProfileS : Profile
    {
        public SprintProfileS()
        {
            CreateMap<SprintS, SprintDTO>();
            CreateMap<SprintCreationDTO, SprintS>();
            CreateMap<SprintUpdateDTO, SprintS>();
            CreateMap<SprintS, SprintS>();
        }
    }
}
