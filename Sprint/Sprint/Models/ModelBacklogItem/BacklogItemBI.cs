using Sprint.Models.ModelBacklog;
using Sprint.Models.ModelPOBI;
using Sprint.Models.ModelSprint;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint.Models.ModelBacklogItem
{
    public class BacklogItemBI
    {
        [Key]
        public Guid BacklogItemId { get; set; }

        #region Backlog Item
        public string TimeAddedBacklogItem { get; set; }


        public Guid BacklogId { get; set; }
        [ForeignKey("BacklogId")]
        public BacklogB Backlog { get; set; }

        public Guid SprintId { get; set; }
        [ForeignKey("SprintId")]
        public SprintS Sprint { get; set; }

        public Guid POBIId { get; set; }
        [ForeignKey("POBIId")]
        public PhaseOfBacklogItem POBI { get; set; }
        #endregion
    }
}
