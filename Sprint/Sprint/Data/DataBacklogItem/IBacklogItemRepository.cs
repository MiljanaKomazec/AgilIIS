using Sprint.Models.ModelBacklog;
using Sprint.Models.ModelBacklogItem;
using Sprint.Models.ModelSprint;

namespace Sprint.Data.DataBacklogItem
{
    public interface IBacklogItemRepository
    {
        Task<List<BacklogItemBI>> GetBacklogItem();
        Task<BacklogItemBI> GetBacklogItemById(Guid biId);
        Task<BacklogItemConfirmation> CreateBacklogItem(BacklogItemBI backlogItem);
        Task UpdateBacklogItem(BacklogItemBI backlogItem);
        Task DeleteBacklogItem(Guid biId);

        Task<List<BacklogItemBI>> GetBacklogItemBySprintId(Guid sprintId);

        Task<List<BacklogItemBI>> GetBacklogItemByPOBIId(Guid pobiId);

        Task<bool> SaveChanges();
    }
}
