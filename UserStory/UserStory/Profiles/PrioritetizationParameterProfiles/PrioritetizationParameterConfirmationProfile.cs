using AutoMapper;
using UserStory.Models.ModelPP;
using UserStory.Models.ModelUserStory;

namespace UserStory.Profiles.PrioritetizationParameterProfiles
{
    public class PrioritetizationParameterConfirmationProfile: Profile
    {
        public PrioritetizationParameterConfirmationProfile() 
        {
            CreateMap<PrioritetizationParameter, PrioritetizationParameterConfirmation>();
            CreateMap<PrioritetizationParameterConfirmation, PrioritetizationParameterConfirmationDTO>();
        }
    }
}
