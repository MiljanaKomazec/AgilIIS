using System.ComponentModel.DataAnnotations;

namespace UserStory.Models.ModelPP
{
    public class PrioritetizationParameterCreationDTO
    {
        public Guid PrioritetizationParameterId { get; set; }
        public int ValueForCustomerPP { get; set; }
        public decimal CostPP { get; set; }
    }
}
