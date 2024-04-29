using AutoMapper;
using UserStory.Models.ModelUserStory;

namespace UserStory.Profiles.UserStoryProfiles
{
    public class UserStoryPrrofile : Profile
    {
        public UserStoryPrrofile()
        {
            CreateMap<UserStoryRoot, UserStoryDTO>()
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

            CreateMap<UserStoryCreationDTO, UserStoryRoot>()
                .ForMember(dest => dest.TextUserStory, opt => opt.MapFrom(src =>
                src.TextUserStory))
                .ForMember(dest => dest.PartOfEpic, opt => opt.MapFrom(src =>
                src.PartOfEpic))
                .ForMember(dest => dest.PrioritetizationParameterId, opt => opt.MapFrom(src =>
                src.PrioritetizationParameterId))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src =>
                src.BacklogId))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src =>
                src.SprintId));

            CreateMap<UserStoryUpdateDTO, UserStoryRoot>()
                .ForMember(dest => dest.UserStoryRootId, opt => opt.MapFrom(src =>
                src.UserStoryRootId))
                .ForMember(dest => dest.TextUserStory, opt => opt.MapFrom(src =>
                src.TextUserStory))
                .ForMember(dest => dest.PartOfEpic, opt => opt.MapFrom(src =>
                src.PartOfEpic))
                .ForMember(dest => dest.PrioritetizationParameterId, opt => opt.MapFrom(src =>
                src.PrioritetizationParameterId))
                .ForMember(dest => dest.PrioritetizationParameter, opt => opt.MapFrom(src =>
                src.PrioritetizationParameter))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src =>
                src.BacklogId))
                 .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src =>
                src.SprintId));

            CreateMap<UserStoryRoot, UserStoryRoot>();
        }
    }
}
