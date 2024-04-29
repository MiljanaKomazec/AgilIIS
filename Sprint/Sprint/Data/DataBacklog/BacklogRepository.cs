using AutoMapper;
using Sprint.Entities;
using Sprint.Models.ModelBacklog;
using Sprint.Models.ModelPOBI;

namespace Sprint.Data.DataBacklog
{
    public class BacklogRepository : IBacklogRepository
    {
        private readonly SprintContext context;
        private readonly IMapper mapper;

        public BacklogRepository(SprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<BacklogB> GetBacklog()
        {
            try
            {
                var obj = context.Backlog.ToList();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public BacklogB GetBacklogById(Guid backlogId)
        {
            return context.Backlog.FirstOrDefault(e => e.BacklogId == backlogId);
        }

        public BacklogConfirmation CreateBacklog(BacklogB backlog)
        {
            var createdEntity = context.Add(backlog);
            return mapper.Map<BacklogConfirmation>(createdEntity.Entity);
        }

        public void UpdateBacklog(BacklogB backlog)
        {

        }

        public void DeleteBacklog(Guid backlogId)
        {
            var backlog = GetBacklogById(backlogId);
            context.Remove(backlog);
        }

    }
}
