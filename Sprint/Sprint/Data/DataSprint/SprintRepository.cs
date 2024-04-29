using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sprint.Entities;
using Sprint.Models.ModelSprint;

namespace Sprint.Data.DataSprint
{
    public class SprintRepository : ISprintRepository
    {
        private readonly SprintContext context;
        private readonly IMapper mapper;

        public SprintRepository(SprintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        
        public List<SprintS> GetSprint()
        {
            try
            {
                var obj = context.Sprint.ToList();
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

        public SprintS GetSprintById(Guid sprintid)
        {
            return context.Sprint.FirstOrDefault(e => e.SprintId == sprintid);
        }

        public SprintConfirmation CreateSprint(SprintS sprint)
        {
            var createdEntity = context.Add(sprint);
            return mapper.Map<SprintConfirmation>(createdEntity.Entity);
        }

        public void DeleteSprint(Guid sprintid)
        {
            var sprint = GetSprintById(sprintid);
            context.Remove(sprint);
        }

        public void UpdateSprint(SprintS sprint)
        {
           
        }

    }
}
