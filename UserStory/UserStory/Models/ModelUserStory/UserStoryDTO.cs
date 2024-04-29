using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UserStory.Models.ModelPP;
using UserStory.Models.OtherServices;

namespace UserStory.Models.ModelUserStory
{
    public class UserStoryDTO
    {
        public Guid UserStoryRootId { get; set; }
        public string TextUserStory { get; set; }
        public string PartOfEpic { get; set; }
        public Guid PrioritetizationParameterId { get; set; }
        public PrioritetizationParameter PrioritetizationParameter { get; set; }

        public Guid BacklogId { get; set; }
        public Guid? SprintId { get; set; }
    }
}
