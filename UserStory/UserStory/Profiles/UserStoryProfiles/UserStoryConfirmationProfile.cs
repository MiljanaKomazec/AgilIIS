using AutoMapper;
using UserStory.Models.ModelUserStory;

namespace UserStory.Profiles.UserStoryProfiles
{
    public class UserStoryConfirmationProfile : Profile
    {
        public UserStoryConfirmationProfile()
        {
            CreateMap<UserStoryRoot, UserStoryConfirmation>()
                .ForMember(dest => dest.UserStoryRootId, opt => opt.MapFrom(src =>
                src.UserStoryRootId))
                .ForMember(dest => dest.TextUserStory, opt => opt.MapFrom(src =>
                src.TextUserStory))
                .ForMember(dest => dest.PartOfEpic, opt => opt.MapFrom(src =>
                src.PartOfEpic))
                .ForMember(dest => dest.PrioritetizationParameterId, opt => opt.MapFrom(src =>
                src.PrioritetizationParameterId))
                .ForMember(dest => dest.PrioritetizationParameter, opt => opt.MapFrom(src =>
                src.PrioritetizationParameter));

            CreateMap<UserStoryConfirmation, UserStoryConfirmationDTO>()
                .ForMember(dest => dest.UserStoryRootId, opt => opt.MapFrom(src =>
                src.UserStoryRootId))
                .ForMember(dest => dest.TextUserStory, opt => opt.MapFrom(src =>
                src.TextUserStory))
                .ForMember(dest => dest.PartOfEpic, opt => opt.MapFrom(src =>
                src.PartOfEpic))
                .ForMember(dest => dest.PrioritetizationParameterId, opt => opt.MapFrom(src =>
                src.PrioritetizationParameterId))
                .ForMember(dest => dest.PrioritetizationParameter, opt => opt.MapFrom(src =>
                src.PrioritetizationParameter));
        }
    }
}
