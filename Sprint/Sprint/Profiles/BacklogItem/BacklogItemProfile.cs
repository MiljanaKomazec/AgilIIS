using AutoMapper;
using Sprint.Models.ModelBacklogItem;

namespace Sprint.Profiles.BacklogItem
{
    public class BacklogItemProfile : Profile
    {
        public BacklogItemProfile() 
        {
            CreateMap<BacklogItemBI, BacklogItemDTO>()
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.TimeAddedBacklogItem, opt => opt.MapFrom(src => src.TimeAddedBacklogItem))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId))
                .ForMember(dest => dest.Backlog, opt => opt.MapFrom(src => src.Backlog))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src => src.SprintId))
                .ForMember(dest => dest.Sprint, opt => opt.MapFrom(src => src.Sprint))
                .ForMember(dest => dest.POBIId, opt => opt.MapFrom(src => src.POBIId))
                .ForMember(dest => dest.POBI, opt => opt.MapFrom(src => src.POBI))
                .ReverseMap();
            CreateMap<BacklogItemCreateDTO, BacklogItemBI>()
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.TimeAddedBacklogItem, opt => opt.MapFrom(src => src.TimeAddedBacklogItem))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src => src.SprintId))
                .ForMember(dest => dest.POBIId, opt => opt.MapFrom(src => src.POBIId))
                .ReverseMap();
            CreateMap<BacklogItemUpdateDTO, BacklogItemBI>()
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.TimeAddedBacklogItem, opt => opt.MapFrom(src => src.TimeAddedBacklogItem))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId))
                .ForMember(dest => dest.Backlog, opt => opt.MapFrom(src => src.Backlog))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src => src.SprintId))
                .ForMember(dest => dest.Sprint, opt => opt.MapFrom(src => src.Sprint))
                .ForMember(dest => dest.POBIId, opt => opt.MapFrom(src => src.POBIId))
                .ForMember(dest => dest.POBI, opt => opt.MapFrom(src => src.POBI))
                .ReverseMap();
            CreateMap<BacklogItemBI, BacklogItemBI>()
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.TimeAddedBacklogItem, opt => opt.MapFrom(src => src.TimeAddedBacklogItem))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId))
                .ForMember(dest => dest.Backlog, opt => opt.MapFrom(src => src.Backlog))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src => src.SprintId))
                .ForMember(dest => dest.Sprint, opt => opt.MapFrom(src => src.Sprint))
                .ForMember(dest => dest.POBIId, opt => opt.MapFrom(src => src.POBIId))
                .ForMember(dest => dest.POBI, opt => opt.MapFrom(src => src.POBI))
                .ReverseMap();
        } 
    }
}
