using AutoMapper;
using CommentService.Model;
using Microsoft.VisualBasic;

namespace CommentService.Profiles
{
    public class CommentConfirmationProfile : Profile
    {
        public CommentConfirmationProfile() 
        {
            CreateMap<CommentConfirmation, CommentConfirmationDTO>();
            CreateMap<Comment, CommentConfirmation>();
        }
    }
}
