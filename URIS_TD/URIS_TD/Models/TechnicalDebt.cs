using System.ComponentModel.DataAnnotations;

namespace URIS_TD.Models
{
    public class TechnicalDebt
    {
        [Key]
        public Guid IdTd { get; set; }
        [Required]
        public string NameTd { get; set; }  
        public string DescriptionTd { get; set; }

        public Guid TypeID { get; set; }

        public Guid SprintId { get; set; }
    }
}
