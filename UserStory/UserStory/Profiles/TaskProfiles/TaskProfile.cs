using AutoMapper;
using UserStory.Models.ModelTask;
using UserStory.Models.ModelUserStory;

namespace UserStory.Profiles.TaskProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile() 
        {
            CreateMap<TaskE, TaskDTO>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src =>
                src.TaskId))
                .ForMember(dest => dest.TextTask, opt => opt.MapFrom(src =>
                src.TextTask))
                .ForMember(dest => dest.FunctionalityId, opt => opt.MapFrom(src =>
                src.FunctionalityId))
                .ForMember(dest => dest.Functionallity, opt => opt.MapFrom(src =>
                src.Functionallity));

            CreateMap<TaskCreationDTO, TaskE>()
                .ForMember(dest => dest.TextTask, opt => opt.MapFrom(src =>
                src.TextTask))
                .ForMember(dest => dest.FunctionalityId, opt => opt.MapFrom(src =>
                src.FunctionalityId))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src =>
                src.SprintId));

            CreateMap<TaskUpdateDTO, TaskE>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src =>
                src.TaskId))
                .ForMember(dest => dest.TextTask, opt => opt.MapFrom(src =>
                src.TextTask))
                .ForMember(dest => dest.FunctionalityId, opt => opt.MapFrom(src =>
                src.FunctionalityId))
                .ForMember(dest => dest.Functionallity, opt => opt.MapFrom(src =>
                src.Functionallity))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src =>
                src.SprintId));
            
            CreateMap<TaskE, TaskE>();

            CreateMap<TaskE, UserStoryDTO>();
        }
    }
}
