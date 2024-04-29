using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserStory.Entities;
using UserStory.Models.ModelFunctionality;
using UserStory.Models.ModelUserStory;

namespace UserStory.Data.DataFunctionallity
{
    public class FunctionalityRepository : IFunctionalityRepository
    {
        private readonly UserStoryContext context;
        private readonly IMapper mapper;

        public FunctionalityRepository(UserStoryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<Functionality>> GetFunctionality()
        {
            var functionalities = await context.Functionallities
                                        .Include(f => f.UserStoryRoot)
                                        .ThenInclude(us => us.PrioritetizationParameter)
                                        .ToListAsync();

            foreach(Functionality functionality in functionalities)
            {
                Console.WriteLine(functionality);
            }
            return functionalities;
        }


        public async Task<Functionality> GetFunctionalityById(Guid functionalityId)
        {
            return await context.Functionallities
                                            .Include(us => us.UserStoryRoot)
                                            .ThenInclude(us => us.PrioritetizationParameter)
                                            .FirstOrDefaultAsync(e => e.FunctionalityId == functionalityId);
        }

        public async Task UpdateFunctionality(Functionality functionality)
        {
            await context.SaveChangesAsync();
        }

        public async  Task<FunctionalityConfirmation> CreateFunctionality(Functionality functionality)
        {
            var createdEntity = await context.AddAsync(functionality);
            await context.SaveChangesAsync();
            return mapper.Map<FunctionalityConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteFunctionality(Guid functionalityId)
        {
            var functionality = await GetFunctionalityById(functionalityId);
            context.Functionallities.Remove(functionality);
            await context.SaveChangesAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<Functionality>> GetFunctionalityByUserStory(Guid userStoryRootId)
        {
            return await context.Functionallities
                                .Include(f => f.UserStoryRoot)
                                .ThenInclude(us => us.PrioritetizationParameter)
                                .Where(us => us.UserStoryRootId == userStoryRootId)
                                .ToListAsync();
        }


        public async Task<List<Functionality>> GetFunctionalitiesBySprintId(Guid sprintId)
        {
            return await context.Functionallities
                .Include(f => f.UserStoryRoot)
                .Where(us => us.SprintId == sprintId)
                .ToListAsync();
        }

    }
}
