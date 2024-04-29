using AutoMapper;
using CommentService.Model;
using Microsoft.VisualBasic;

namespace CommentService.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile() 
        {
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentCreateDTO, Comment>();
            CreateMap<CommentUpdateDTO, Comment>();
            CreateMap<Comment, Comment>();

        }
    }
}
