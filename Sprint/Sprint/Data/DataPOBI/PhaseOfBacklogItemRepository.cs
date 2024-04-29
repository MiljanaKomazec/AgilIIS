using AutoMapper;
using Sprint.Entities;
using Sprint.Migrations;
using Sprint.Models.ModelPOBI;
using Sprint.Models.ModelSprint;

namespace Sprint.Data.DataPOBI
{
    public class PhaseOfBacklogItemRepository : IPhaseOfBacklogItemRepository
    {

        private readonly SprintContext context;
        private readonly IMapper mapper;

        public PhaseOfBacklogItemRepository(SprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<PhaseOfBacklogItem> GetPhaseOfBacklogItem()
        {
            try
            {
                var obj = context.PhaseOfBacklogItem.ToList();
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

        public PhaseOfBacklogItem GetPhaseOfBacklogItemById(Guid pobiId)
        {
            return context.PhaseOfBacklogItem.FirstOrDefault(e => e.POBIId == pobiId);
        }

        public PhaseOfBacklogItemConfirmation CreatePhaseOfBacklogItem(PhaseOfBacklogItem pobi)
        {
            var createdEntity = context.Add(pobi);
            return mapper.Map<PhaseOfBacklogItemConfirmation>(createdEntity.Entity);
        }

        public void UpdatePhaseOfBacklogItem(PhaseOfBacklogItem pobi)
        {

        }

        public void DeletePhaseOfBacklogItem(Guid pobiId)
        {
            var pobi = GetPhaseOfBacklogItemById(pobiId);
            context.Remove(pobi);
        }
    }
}
