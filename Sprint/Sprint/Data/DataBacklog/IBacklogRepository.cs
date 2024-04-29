using Sprint.Models.ModelBacklog;

namespace Sprint.Data.DataBacklog
{
    public interface IBacklogRepository
    {
        List<BacklogB> GetBacklog();
        BacklogB GetBacklogById(Guid backlogId);
        BacklogConfirmation CreateBacklog(BacklogB backlog);
        void UpdateBacklog(BacklogB backlog);
        void DeleteBacklog(Guid backlogId);

        bool SaveChanges();
    }
}
