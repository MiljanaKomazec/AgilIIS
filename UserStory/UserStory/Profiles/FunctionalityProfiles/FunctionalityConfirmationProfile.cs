using AutoMapper;
using UserStory.Models.ModelFunctionality;
using UserStory.Models.ModelUserStory;

namespace UserStory.Profiles.FunctionalityProfiles
{
    public class FunctionalityConfirmationProfile: Profile
    {
        public FunctionalityConfirmationProfile() 
        {
            CreateMap<Functionality, FunctionalityConfirmation>()
                .ForMember(dest => dest.FunctionalityId, opt => opt.MapFrom(src =>
                src.FunctionalityId))
                .ForMember(dest => dest.TextFunctionality, opt => opt.MapFrom(src =>
                src.TextFunctionality))
                .ForMember(dest => dest.UserStoryRootId, opt => opt.MapFrom(src =>
                src.UserStoryRootId))
                .ForMember(dest => dest.UserStoryRoot, opt => opt.MapFrom(src =>
                src.UserStoryRoot)); 

            CreateMap<FunctionalityConfirmation, FunctionalityConfirmationDTO>()
                .ForMember(dest => dest.FunctionalityId, opt => opt.MapFrom(src =>
                src.FunctionalityId))
                .ForMember(dest => dest.TextFunctionality, opt => opt.MapFrom(src =>
                src.TextFunctionality))
                .ForMember(dest => dest.UserStoryRootId, opt => opt.MapFrom(src =>
                src.UserStoryRootId))
                .ForMember(dest => dest.UserStoryRoot, opt => opt.MapFrom(src =>
                src.UserStoryRoot));
        }
    }
}
