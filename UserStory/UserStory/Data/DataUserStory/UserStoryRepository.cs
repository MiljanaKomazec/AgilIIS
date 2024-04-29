using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserStory.Entities;
using UserStory.Models.ModelUserStory;

namespace UserStory.Data.DataUserStory
{
    public class UserStoryRepository : IUserStoryRepository
    {
        private readonly UserStoryContext context;
        private readonly IMapper mapper;

        public UserStoryRepository(UserStoryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //async - asinhrone metode: one koje se mogu izvrsavati na odvojenom thread-u
        //omogucava pozivajucem kodu da se nastavi dok asihrona operacija traje (izbegava se blokiranje glavnog thread-a)
        //await - asihrono cekanje izvrsenja asihrone operacije
        public async Task<List<UserStoryRoot>> GetUserStory()
        {
            return await context.UserStories.Include(p => p.PrioritetizationParameter).ToListAsync();
        }
        public async Task<UserStoryRoot> GetUserStoryById(Guid userStoryId)
        {
            return await context.UserStories
                .Include(pp => pp.PrioritetizationParameter)
                .FirstOrDefaultAsync(u => u.UserStoryRootId == userStoryId);
        }

        public async Task<UserStoryConfirmation> CreateUserStory(UserStoryRoot userStory)
        {
            var createdEntity = await context.AddAsync(userStory);
            await context.SaveChangesAsync();
            return mapper.Map<UserStoryConfirmation>(createdEntity.Entity);
        }

        public async Task UpdateUserStory(UserStoryRoot userStory)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserStory(Guid userStoryId)
        {
            var userStory = await GetUserStoryById(userStoryId);
            context.UserStories.Remove(userStory);
            await context.SaveChangesAsync();
        }

         public async Task<bool> SaveChanges()
        {
            return  await context.SaveChangesAsync() > 0;
        }

        public async Task<List<UserStoryRoot>> GetUserStoriesByBacklogId(Guid backlogId)
        {
            return await context.UserStories
                .Include(pp => pp.PrioritetizationParameter)
                .Where(us => us.BacklogId == backlogId)
                .ToListAsync();
        }

        public async Task<List<UserStoryRoot>> GetUserStoriesBySprintId(Guid sprintId)
        {
            return await context.UserStories
                .Include(pp => pp.PrioritetizationParameter)
                .Where(us => us.SprintId == sprintId)
                .ToListAsync();
        }
    }
}
