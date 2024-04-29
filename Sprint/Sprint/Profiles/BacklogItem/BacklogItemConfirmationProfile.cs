using AutoMapper;
using Sprint.Models.ModelBacklogItem;

namespace Sprint.Profiles.BacklogItem
{
    public class BacklogItemConfirmationProfile : Profile
    {
        public BacklogItemConfirmationProfile() 
        {
            CreateMap<BacklogItemConfirmation, BacklogItemConfirmationDTO>()
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.TimeAddedBacklogItem, opt => opt.MapFrom(src => src.TimeAddedBacklogItem))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src => src.SprintId))
                .ForMember(dest => dest.POBIId, opt => opt.MapFrom(src => src.POBIId)); 
            CreateMap<BacklogItemBI, BacklogItemConfirmation>()
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.TimeAddedBacklogItem, opt => opt.MapFrom(src => src.TimeAddedBacklogItem))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId))
                .ForMember(dest => dest.SprintId, opt => opt.MapFrom(src => src.SprintId))
                .ForMember(dest => dest.POBIId, opt => opt.MapFrom(src => src.POBIId)); 
        }
    }
}
