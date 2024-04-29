namespace CommentService.Model
{
    public class CommentDTO
    {
        public Guid CommentId { get; set; }
        public DateTime DateComment { get; set; }
        public string TextComment { get; set; }

        public Guid? UserStoryRootId { get; set; }
        public Guid? UserId { get; set; }
    }
}
