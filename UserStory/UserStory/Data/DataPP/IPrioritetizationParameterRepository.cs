using UserStory.Models.ModelPP;

namespace UserStory.Data.DataPP
{
    public interface IPrioritetizationParameterRepository
    {
        List<PrioritetizationParameter> GetPrioritetizationParameter();
        PrioritetizationParameter GetPrioritetizationParameterById(Guid prioritetId);
        PrioritetizationParameterConfirmation CreatePrioritetizationParameter(PrioritetizationParameter prioritetizationParameter);
        void UpdatePrioritetizationParameter(PrioritetizationParameter prioritetizationParameter);
        void DeletePrioritetizationParameter(Guid prioritetId);

        bool SaveChanges();
    }
}
