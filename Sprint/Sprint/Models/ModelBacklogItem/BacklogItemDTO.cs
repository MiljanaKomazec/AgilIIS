using Sprint.Models.ModelBacklog;
using Sprint.Models.ModelPOBI;
using Sprint.Models.ModelSprint;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint.Models.ModelBacklogItem
{
    public class BacklogItemDTO
    {
        public Guid BacklogItemId { get; set; }
        public string TimeAddedBacklogItem { get; set; }


        public Guid BacklogId { get; set; }
        public BacklogB Backlog { get; set; }

        public Guid SprintId { get; set; }
        public SprintS Sprint { get; set; }

        public Guid POBIId { get; set; }
        public PhaseOfBacklogItem POBI { get; set; }
    }
}
