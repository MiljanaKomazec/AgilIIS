using AutoMapper;
using CommentService.Entiti;
using CommentService.Model;

namespace CommentService.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentContext context;
        private readonly IMapper mapper;

        public CommentRepository(CommentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Comment> GetComment()
        {
            try
            {
                var obj = context.Comment.ToList();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Comment GetCommentById(Guid commentId)
        {
            return context.Comment.FirstOrDefault(e => e.CommentId == commentId);
        }

        public CommentConfirmation CreateComment(Comment comment)
        {
            var createdEntity = context.Add(comment);
            return mapper.Map<CommentConfirmation>(createdEntity.Entity);
        }

        public void UpdateComment(Comment comment)
        {
            
        }

        public void DeleteComment(Guid commentId)
        {
            var comment = GetCommentById(commentId);
            context.Remove(comment);
        }

        public List<Comment> GetCommentByUserStoryId(Guid userStoryRootId)
        {
            return context.Comment.Where(e => e.UserStoryRootId == userStoryRootId).ToList();
        }

        public List<Comment> GetCommentByUserId(Guid userId)
        {
            return context.Comment.Where(e => e.UserId == userId).ToList();
        }
    }
}
