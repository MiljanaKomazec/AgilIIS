using AutoMapper;
using Sprint.Models.ModelPOBI;

namespace Sprint.Profiles.POBI
{
    public class PhaseOfBacklogItemProfile : Profile
    {
        public PhaseOfBacklogItemProfile() 
        {
            CreateMap<PhaseOfBacklogItem, PhaseOfBacklogItemDTO>();
            CreateMap<PhaseOfBacklogItemCreateDTO, PhaseOfBacklogItem>();
            CreateMap<PhaseOfBacklogItemUpdateDTO, PhaseOfBacklogItem>();
            CreateMap<PhaseOfBacklogItem, PhaseOfBacklogItem>();
        }
    }
}
