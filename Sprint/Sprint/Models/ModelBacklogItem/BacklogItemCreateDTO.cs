using Sprint.Models.ModelBacklog;
using Sprint.Models.ModelPOBI;
using Sprint.Models.ModelSprint;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint.Models.ModelBacklogItem
{
    public class BacklogItemCreateDTO
    {
        public Guid BacklogItemId { get; set; }
        public string TimeAddedBacklogItem { get; set; }


        public Guid BacklogId { get; set; }

        public Guid SprintId { get; set; }

        public Guid POBIId { get; set; }

    }
}
