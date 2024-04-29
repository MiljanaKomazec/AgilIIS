using AutoMapper;
using UserStory.Models.ModelTask;
using UserStory.Models.ModelUserStory;

namespace UserStory.Profiles.TaskProfiles
{
    public class TaskConfirmationProfile: Profile
    {
        public TaskConfirmationProfile() 
        {
            CreateMap<TaskE, TaskConfirmation>()
                 .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src =>
                src.TaskId))
                .ForMember(dest => dest.TextTask, opt => opt.MapFrom(src =>
                src.TextTask))
                .ForMember(dest => dest.FunctionalityId, opt => opt.MapFrom(src =>
                src.FunctionalityId))
                .ForMember(dest => dest.Functionallity, opt => opt.MapFrom(src =>
                src.Functionallity)); 

            CreateMap<TaskConfirmation, TaskConfirmationDTO>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src =>
                src.TaskId))
                .ForMember(dest => dest.TextTask, opt => opt.MapFrom(src =>
                src.TextTask))
                .ForMember(dest => dest.FunctionalityId, opt => opt.MapFrom(src =>
                src.FunctionalityId))
                .ForMember(dest => dest.Functionallity, opt => opt.MapFrom(src =>
                src.Functionallity));
        }
    }
}
