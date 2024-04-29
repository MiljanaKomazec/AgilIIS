using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class TaskDTO
    {
        public Guid TaskId { get; set; }
        public string TextTask { get; set; }

        public Guid FunctionalityId { get; set; }

    }
}
