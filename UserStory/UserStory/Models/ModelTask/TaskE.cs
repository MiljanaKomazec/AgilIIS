using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserStory.Models.ModelFunctionality;

namespace UserStory.Models.ModelTask
{
    public class TaskE
    {
        [Key]
        public Guid TaskId { get; set; }

        [Required]
        [MaxLength(200)]
        public string TextTask { get; set; }

        public Guid FunctionalityId { get; set; }
        [ForeignKey("FunctionalityId")]
        public Functionality Functionallity { get; set; }


        //veza sa Sprint-om
        public Guid? SprintId { get; set; }
    }
}
