using AutoMapper;
using Sprint.Models.ModelBacklog;

namespace Sprint.Profiles.Backlog
{
    public class BacklogConfirmationProfile : Profile
    {
        public BacklogConfirmationProfile() 
        {
            CreateMap<BacklogConfirmation, BacklogConfirmationDTO>();
            CreateMap<BacklogB, BacklogConfirmation>();
        }
    }
}
