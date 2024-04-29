using UserStory.Models.ModelFunctionality;
using UserStory.Models.ModelTask;
using UserStory.Models.ModelUserStory;

namespace UserStory.Data.DataFunctionallity
{
    public interface IFunctionalityRepository
    {
        //List<Functionality> GetFunctionality();
        Task<List<Functionality>> GetFunctionality();
        Task<Functionality> GetFunctionalityById(Guid functionalityId);
        Task<FunctionalityConfirmation> CreateFunctionality(Functionality functionality);
        Task UpdateFunctionality(Functionality functionality);
        Task DeleteFunctionality(Guid functionalityId);

        Task<bool> SaveChanges();

        //Vraca sve funkcionalnosti za odredjenu korisnicku pricu
        Task<List<Functionality>> GetFunctionalityByUserStory(Guid userStoryRootId);

        //metoda koja ce na osnovu SprintId-a izlistavati sve funkcionalnosti za taj id
        Task<List<Functionality>> GetFunctionalitiesBySprintId(Guid sprintId);
    }
}
