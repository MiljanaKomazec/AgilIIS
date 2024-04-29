using System.ComponentModel.DataAnnotations;

namespace CommentService.Model
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; }

        #region Comment
        public DateTime DateComment { get; set; }
        public string TextComment { get; set; }

        public Guid? UserStoryRootId { get; set; }
        public Guid? UserId { get; set; }
        #endregion
    }
}
