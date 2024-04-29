using AutoMapper;
using Sprint.Models.ModelPOBI;


namespace Sprint.Profiles.POBI
{
    public class PhaseOfBacklogItemConfirmationProfile : Profile
    {
        public PhaseOfBacklogItemConfirmationProfile() 
        {
            CreateMap<PhaseOfBacklogItemConfirmation, PhaseOfBacklogItemConfirmationDTO>();
            CreateMap<PhaseOfBacklogItem, PhaseOfBacklogItemConfirmation>();
        }
    }
}
