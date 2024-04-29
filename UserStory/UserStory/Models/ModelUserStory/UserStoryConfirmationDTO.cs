using System.ComponentModel.DataAnnotations.Schema;
using UserStory.Models.ModelPP;

namespace UserStory.Models.ModelUserStory
{
    public class UserStoryConfirmationDTO
    {
        public Guid UserStoryRootId { get; set; }
        public string TextUserStory { get; set; }
        public string PartOfEpic { get; set; }

        public Guid PrioritetizationParameterId { get; set; }
        public PrioritetizationParameter PrioritetizationParameter { get; set; }
    }
}
