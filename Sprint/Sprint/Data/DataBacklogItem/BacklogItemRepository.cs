using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sprint.Entities;
using Sprint.Migrations;
using Sprint.Models.ModelBacklog;
using Sprint.Models.ModelBacklogItem;
using Sprint.Models.ModelSprint;

namespace Sprint.Data.DataBacklogItem
{
    public class BacklogItemRepository : IBacklogItemRepository
    {
        private readonly SprintContext context;
        private readonly IMapper mapper;

        public BacklogItemRepository(SprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<BacklogItemBI>> GetBacklogItem()
        {
            
            var backlogItems = await context.BacklogItem.Include(s=>s.Sprint)
                                                                       .Include(b => b.Backlog)
                                                                       .Include(pobi => pobi.POBI)
                                                                       .ToListAsync();
            
            foreach (BacklogItemBI backlogItem in backlogItems)
            {
                Console.WriteLine(backlogItem);
            }
            return backlogItems;
            
        }

        public async Task<BacklogItemBI> GetBacklogItemById(Guid biId)
        {
            var backlogItem = await context.BacklogItem.Include(s => s.Sprint)
                                                                       .Include(b => b.Backlog)
                                                                       .Include(pobi => pobi.POBI)
                                                                       .ToListAsync();
            return backlogItem.FirstOrDefault(e => e.BacklogItemId == biId);
            
        }

        public async Task<BacklogItemConfirmation> CreateBacklogItem(BacklogItemBI backlogItem)
        {
            var createdEntity = await context.AddAsync(backlogItem);
            await context.SaveChangesAsync();
            return mapper.Map<BacklogItemConfirmation>(createdEntity.Entity);
        }

        public async Task UpdateBacklogItem(BacklogItemBI backlogItem)
        {
           await context.SaveChangesAsync();
        }

        public async Task DeleteBacklogItem(Guid biId)
        {
            var backlogItem = await GetBacklogItemById(biId);
            context.BacklogItem.Remove(backlogItem);
            await context.SaveChangesAsync();
        }

        public async Task<List<BacklogItemBI>> GetBacklogItemBySprintId(Guid sprintId)
        {
            var backlogItems = await context.BacklogItem.AsNoTracking().Include(s => s.Sprint)
                                                                       .Include(b => b.Backlog)
                                                                       .Include(pobi => pobi.POBI)
                                                                       .Where(sId => sId.SprintId == sprintId)
                                                                       .ToListAsync();
            foreach (BacklogItemBI backlogItem in backlogItems)
            {
                Console.WriteLine(backlogItem);
            }
            return backlogItems;
        }

        public async Task<List<BacklogItemBI>> GetBacklogItemByPOBIId(Guid pobiId)
        {
            var backlogItems = await context.BacklogItem.AsNoTracking().Include(s => s.Sprint)
                                                                       .Include(b => b.Backlog)
                                                                       .Include(pobi => pobi.POBI)
                                                                       .Where(pobi => pobi.POBIId == pobiId)
                                                                       .ToListAsync();
            foreach (BacklogItemBI backlogItem in backlogItems)
            {
                Console.WriteLine(backlogItem);
            }
            return backlogItems;
  
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
