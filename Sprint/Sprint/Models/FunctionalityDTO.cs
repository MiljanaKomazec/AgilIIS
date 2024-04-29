using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class FunctionalityDTO
    {
        
        public Guid FunctionalityId { get; set; }
        public string TextFunctionality { get; set; }

        public Guid UserStoryRootId { get; set; }
    }
}
