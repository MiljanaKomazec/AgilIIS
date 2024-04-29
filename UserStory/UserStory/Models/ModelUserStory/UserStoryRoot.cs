using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserStory.Models.ModelFunctionality;
using UserStory.Models.ModelPP;

namespace UserStory.Models.ModelUserStory
{
    public class UserStoryRoot
    {
        [Key]
        public Guid UserStoryRootId { get; set; }

        [Required]
        [MaxLength(200)]
        public string TextUserStory { get; set; }
        [Required]
        [MaxLength(200)]
        public string PartOfEpic { get; set; }

        //strani kljuc
        public Guid PrioritetizationParameterId { get; set; }
        [ForeignKey("PrioritetizationParameterId")]
        public PrioritetizationParameter PrioritetizationParameter { get; set; }
        

        //veza sa Backlog-om
        public Guid BacklogId { get; set; }

        //veza sa Sprint-om
        public Guid? SprintId { get; set; }
    }
}
