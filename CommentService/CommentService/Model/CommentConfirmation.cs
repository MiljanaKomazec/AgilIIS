namespace CommentService.Model
{
    public class CommentConfirmation
    {
        public Guid CommentId { get; set; }

        public DateTime DateComment { get; set; }
        public string TextComment { get; set; }
    }
}
