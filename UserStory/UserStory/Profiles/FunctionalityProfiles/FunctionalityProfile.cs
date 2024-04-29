using AutoMapper;
using UserStory.Models.ModelFunctionality;
using UserStory.Models.ModelUserStory;

namespace UserStory.Profiles.FunctionalityProfiles
{
    public class FunctionalityProfile: Profile
    {
        public FunctionalityProfile() 
        {
            CreateMap<Functionality, FunctionalityDTO>()
                .ForMember(dest => dest.FunctionalityId, opt => opt.MapFrom(src =>
                src.FunctionalityId))
                .ForMember(dest => dest.TextFunctionality, opt => opt.MapFrom(src =>
                src.TextFunctionality))
                .ForMember(dest => dest.UserStoryRootId, opt => opt.MapFrom(src =>
                src.UserStoryRootId))
                .ForMember(dest => dest.UserStoryRoot, opt => opt.MapFrom(src =>
                src.UserStoryRoot));

            CreateMap<FunctionalityCreationDTO, Functionality>()
                .ForMember(dest => dest.TextFunctionality, opt => opt.MapFrom(src =>
                src.TextFunctionality))
                .ForMember(dest => dest.UserStoryRootId, opt => opt.MapFrom(src =>
                src.UserStoryRootId))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src =>
                src.SprintId));

            CreateMap<FunctionalityUpdateDTO, Functionality>()
                .ForMember(dest => dest.FunctionalityId, opt => opt.MapFrom(src =>
                src.FunctionalityId))
                .ForMember(dest => dest.TextFunctionality, opt => opt.MapFrom(src =>
                src.TextFunctionality))
                .ForMember(dest => dest.UserStoryRootId, opt => opt.MapFrom(src =>
                src.UserStoryRootId))
                .ForMember(dest => dest.UserStoryRoot, opt => opt.MapFrom(src =>
                src.UserStoryRoot))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src =>
                src.SprintId));

            CreateMap<Functionality, Functionality>();
        }
    }
}
