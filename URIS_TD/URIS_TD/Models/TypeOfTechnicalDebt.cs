using System.ComponentModel.DataAnnotations;

namespace URIS_TD.Models
{
    public class TypeOfTechnicalDebt
    {
        [Key]
        public Guid IdTod {  get; set; }
        [Required]
        public string NameTotd { get; set; }
    }
}
