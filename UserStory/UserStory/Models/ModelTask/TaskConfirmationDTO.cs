using UserStory.Models.ModelFunctionality;

namespace UserStory.Models.ModelTask
{
    public class TaskConfirmationDTO
    {
        public Guid TaskId { get; set; }
        public string TextTask { get; set; }

        public Guid FunctionalityId { get; set; }
        public Functionality Functionallity { get; set; }
    }
}
