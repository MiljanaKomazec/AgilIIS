using UserStory.Models.ModelUserStory;

namespace UserStory.Models.ModelFunctionality
{
    public class FunctionalityConfirmationDTO
    {
        public Guid FunctionalityId { get; set; }
        public string TextFunctionality { get; set; }
        public Guid UserStoryRootId { get; set; }
        public UserStoryRoot UserStoryRoot { get; set; }
    }
}
