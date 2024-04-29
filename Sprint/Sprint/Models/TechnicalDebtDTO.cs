using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class TechnicalDebtDTO
    {  
        public Guid IdTd { get; set; }
        public string NameTd { get; set; }
        public string DescriptionTd { get; set; }

        public Guid TypeID { get; set; }
    }
}
