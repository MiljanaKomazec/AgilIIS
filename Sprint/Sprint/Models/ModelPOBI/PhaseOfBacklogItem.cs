using System.ComponentModel.DataAnnotations;

namespace Sprint.Models.ModelPOBI
{
    public class PhaseOfBacklogItem
    {
        [Key]
        public Guid POBIId { get; set; }

        #region PhaseOfBacklogItem
        public string NameOfPOBI { get; set; }
        #endregion
    }
}
