using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserStory.Entities;
using UserStory.Models.ModelFunctionality;
using UserStory.Models.ModelTask;
using UserStory.Models.ModelUserStory;

namespace UserStory.Data.DataTask
{
    public class TaskRepository : ITaskRepository
    {
        private readonly UserStoryContext context;
        private readonly IMapper mapper;

        public TaskRepository(UserStoryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<TaskE>> GetTask()
        {
            //var tasks = await context.Tasks.Include(f => f.Functionallity).ToListAsync();

            var tasks = await context.Tasks
                                .Include(t => t.Functionallity)
                                .ThenInclude(f => f.UserStoryRoot)
                                .ThenInclude(us => us.PrioritetizationParameter)
                                .ToListAsync();

            foreach (TaskE task in tasks)
            {
                Console.WriteLine(task);
            }
            return tasks;
        }

        public async Task<TaskE> GetTaskById(Guid taskId)
        {
            return await context.Tasks
                                .Include(t => t.Functionallity)
                                .ThenInclude(f => f.UserStoryRoot)
                                .ThenInclude(us => us.PrioritetizationParameter)
                                .FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task<TaskConfirmation> CreateTask(TaskE task)
        {
            var createdEntity = await context.AddAsync(task);
            await context.SaveChangesAsync();
            return mapper.Map<TaskConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteTask(Guid taskId)
        {
            var userStory =await GetTaskById(taskId);
            context.Tasks.Remove(userStory);
            await context.SaveChangesAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateTask(TaskE task)
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<TaskE>> GetTaskByFunctionality(Guid functionalityId)
        {
            //return context.Tasks.Where(f => f.FunctionalityId == functionalityId).ToList();

            return await context.Tasks
                            .Include(t => t.Functionallity)
                            .ThenInclude(f => f.UserStoryRoot)
                            .Where(f => f.FunctionalityId == functionalityId)
                            .ToListAsync();
        }


        public async Task<List<TaskE>> GetTasksBySprintId(Guid sprintId)
        {
            return await context.Tasks
                .Include(t => t.Functionallity)
                .Where(f => f.SprintId == sprintId)
                .ToListAsync();
        }

    }
}
