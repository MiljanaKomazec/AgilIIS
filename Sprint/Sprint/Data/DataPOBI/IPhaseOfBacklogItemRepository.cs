using Sprint.Models.ModelPOBI;

namespace Sprint.Data.DataPOBI
{
    public interface IPhaseOfBacklogItemRepository
    {
        List<PhaseOfBacklogItem> GetPhaseOfBacklogItem();
        PhaseOfBacklogItem GetPhaseOfBacklogItemById(Guid pobiId);
        PhaseOfBacklogItemConfirmation CreatePhaseOfBacklogItem(PhaseOfBacklogItem pobi);
        void UpdatePhaseOfBacklogItem(PhaseOfBacklogItem pobi);
        void DeletePhaseOfBacklogItem(Guid pobiId);
        bool SaveChanges();
    }
}
