using System.ComponentModel.DataAnnotations;

namespace UserStory.Models.ModelPP
{
    public class PrioritetizationParameter
    {
        [Key]
        public Guid PrioritetizationParameterId { get; set; }
        public int ValueForCustomerPP { get; set; }
        public decimal CostPP { get; set; }
    }
}
