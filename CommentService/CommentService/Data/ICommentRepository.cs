using CommentService.Model;

namespace CommentService.Data
{
    public interface ICommentRepository
    {
        List<Comment> GetComment();
        Comment GetCommentById(Guid commentId);
        CommentConfirmation CreateComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(Guid commentId);

        List<Comment> GetCommentByUserStoryId(Guid userStoryRootId);
        List<Comment> GetCommentByUserId(Guid userId);

        bool SaveChanges();
    }
}
