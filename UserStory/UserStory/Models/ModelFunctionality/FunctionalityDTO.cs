using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UserStory.Models.ModelUserStory;

namespace UserStory.Models.ModelFunctionality
{
    public class FunctionalityDTO
    {
        public Guid FunctionalityId { get; set; }
        public string TextFunctionality { get; set; }
        public Guid UserStoryRootId { get; set; }
        public UserStoryRoot UserStoryRoot { get; set; }

        public Guid? SprintId { get; set; }
    }
}
