using WebApplication5.DTO;

namespace WebApplication5.ServiceCalls
{
    public interface IServiceCalls
    {
        public Task<List<CommentDTO>> GetCommentByUserId(Guid userId);
    }
}
