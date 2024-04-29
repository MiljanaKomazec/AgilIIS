using System.ComponentModel.DataAnnotations;

namespace Sprint.Models.ModelSprint
{
    public class SprintS
    {
        [Key]
        public Guid SprintId { get; set; }

        #region Sprint
        public string DurationSprint { get; set; }
        public DateTime StartOfSprint { get; set; }
        public DateTime EndOfSprint { get; set; }
        #endregion
    }
}
