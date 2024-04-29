using AutoMapper;
using UserStory.Models.ModelPP;
using UserStory.Models.ModelUserStory;

namespace UserStory.Profiles.PrioritetizationParameterProfiles
{
    public class PrioritetizationParameterProfile: Profile
    {
        public PrioritetizationParameterProfile() 
        {
            CreateMap<PrioritetizationParameter, PrioritetizationParameterDTO>();
            CreateMap<PrioritetizationParameterCreationDTO, PrioritetizationParameter>().ReverseMap();
            CreateMap<PrioritetizationParameterUpdateDTO, PrioritetizationParameter>().ReverseMap();
            CreateMap<PrioritetizationParameter, PrioritetizationParameter>();
        }
    }
}
