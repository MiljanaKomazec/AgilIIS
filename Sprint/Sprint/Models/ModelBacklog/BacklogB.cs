using System.ComponentModel.DataAnnotations;

namespace Sprint.Models.ModelBacklog
{
    public class BacklogB
    {
        [Key]
        public Guid BacklogId { get; set; }

        #region Backlog
        public string NameBacklog { get; set; }

        #endregion
    }
}
