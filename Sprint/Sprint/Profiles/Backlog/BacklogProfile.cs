using AutoMapper;
using Sprint.Models.ModelBacklog;

namespace Sprint.Profiles.Backlog
{
    public class BlogProfile : Profile
    {
        public BlogProfile() 
        {
            CreateMap<BacklogB, BacklogDTO>();
            CreateMap<BacklogCreateDTO, BacklogB>();
            CreateMap<BacklogUpdateDTO, BacklogB>();
            CreateMap<BacklogB, BacklogB>();
        }
    }
}
