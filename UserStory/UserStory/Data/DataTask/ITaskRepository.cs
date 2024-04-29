using UserStory.Models.ModelFunctionality;
using UserStory.Models.ModelTask;

namespace UserStory.Data.DataTask
{
    public interface ITaskRepository
    {
        //List<TaskE> GetTask();
        
        Task<List<TaskE>> GetTask();
        Task<TaskE> GetTaskById(Guid taskId);
        Task<TaskConfirmation> CreateTask(TaskE task);
        Task UpdateTask(TaskE task);
        Task DeleteTask(Guid taskId);

        Task<bool> SaveChanges();

        //Vraca sve taskove za odredjenu funkcionalnost
        Task<List<TaskE>> GetTaskByFunctionality(Guid functionalityId);

        //metoda koja ce na osnovu SprintId-a izlistavati sve taskove za taj id
        Task<List<TaskE>> GetTasksBySprintId(Guid sprintId);
    }
}
