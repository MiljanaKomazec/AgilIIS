namespace CommentService.Model
{
    public class UserStoryDTO
    {
        public Guid UserStoryRootId { get; set; }
        public string TextUserStory { get; set; }
        public string PartOfEpic { get; set; }
    }
}
