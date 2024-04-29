using UserStory.Models.ModelUserStory;

namespace UserStory.Data.DataUserStory
{
    public interface IUserStoryRepository
    {
        //List<UserStoryRoot> GetUserStory();
        Task<List<UserStoryRoot>> GetUserStory();
        Task<UserStoryRoot> GetUserStoryById(Guid userStoryId);
        Task<UserStoryConfirmation> CreateUserStory(UserStoryRoot userStory);
        Task UpdateUserStory(UserStoryRoot userStory);
        Task DeleteUserStory(Guid userStoryId);

        Task<bool> SaveChanges();



        //metoda koja ce na osnovu BacklogId-a izlistavati sve korisnicke price za taj id
        Task<List<UserStoryRoot>> GetUserStoriesByBacklogId(Guid backlogId);

        //metoda koja ce na osnovu SprintId-a izlistavati sve korisnicke price za taj id
        Task<List<UserStoryRoot>> GetUserStoriesBySprintId(Guid sprintId);
    }
}
