using Sprint.Models;

namespace Sprint.ServiceCalls
{
    public interface IServiceCalls
    {
        public Task<List<UserStoryDTO>> GetUserStoriesByBacklogId(Guid backlogId);
        public Task<List<UserStoryDTO>> GetUserStoriesBySprintId(Guid sprintId);
        public Task<List<FunctionalityDTO>> GetFunctionalityBySprintId(Guid sprintId);
        public Task<List<TaskDTO>> GetTaskBySprintId(Guid sprintId);

        public Task<List<TechnicalDebtDTO>> GetTdBySprintId(Guid sprintId);
    }
}
