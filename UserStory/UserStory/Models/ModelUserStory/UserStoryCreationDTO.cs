using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UserStory.Models.ModelPP;

namespace UserStory.Models.ModelUserStory
{
    public class UserStoryCreationDTO: IValidatableObject
    {
        //public Guid UserStoryRootId { get; set; }

        [Required(ErrorMessage = "It is necessary to enter the text of the user's story")]
        [MaxLength(200)]
        public string TextUserStory { get; set; }
        [Required]
        [MaxLength(200)]
        public string PartOfEpic { get; set; }
        public Guid PrioritetizationParameterId { get; set; }
        //[ForeignKey("PrioritetizationParameterId")]
        //public PrioritetizationParameter PrioritetizationParameter { get; set; }

        public Guid BacklogId { get; set; }
        public Guid? SprintId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TextUserStory == null) 
            {
                yield return new ValidationResult(
                    "Can't create a user story until specific story text is entered",
                    new[] { "UserStoryCreationDTO" });
            }
            if (TextUserStory == PartOfEpic)
            {
                yield return new ValidationResult(
                    "TextUserStory and PartOfEpic must not be equal",
                    new[] { "UserStoryCreationDTO" });
            }
        }
    }
}
