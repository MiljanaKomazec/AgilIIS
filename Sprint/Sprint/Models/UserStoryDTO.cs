namespace Sprint.Models
{
    public class UserStoryDTO
    {
        public Guid UserStoryRootId { get; set; }
        public string TextUserStory { get; set; }
        public string PartOfEpic { get; set; }
        public Guid PrioritetizationParameterId { get; set; }
        //public PrioritetizationParameter PrioritetizationParameter { get; set; }
    }
}
