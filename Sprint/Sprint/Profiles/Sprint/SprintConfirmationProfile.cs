using AutoMapper;
using Sprint.Models.ModelSprint;

namespace Sprint.Profiles.SprintProfile.SprintProfile
{
    public class SprintConfirmationProfile : Profile
    {
        public SprintConfirmationProfile()
        {
            CreateMap<SprintConfirmation, SprintConfirmationDTO>();
            CreateMap<SprintS, SprintConfirmation>();
        }
    }
}
