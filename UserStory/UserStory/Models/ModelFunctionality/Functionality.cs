using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserStory.Models.ModelUserStory;

namespace UserStory.Models.ModelFunctionality
{
    public class Functionality
    {
        [Key]
        public Guid FunctionalityId { get; set; }

        [Required]
        [MaxLength(200)]
        public string TextFunctionality { get; set; }

        //strani kljuc
        public Guid UserStoryRootId { get; set; }
        [ForeignKey("UserStoryRootId")]
        public UserStoryRoot UserStoryRoot { get; set; }

        //veza sa Sprint-om
        public Guid? SprintId { get; set; }
    }
}
