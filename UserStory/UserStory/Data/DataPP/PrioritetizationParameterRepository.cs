using AutoMapper;
using System.Threading.Tasks;
using UserStory.Entities;
using UserStory.Models.ModelPP;
using UserStory.Models.ModelTask;

namespace UserStory.Data.DataPP
{
    public class PrioritetizationParameterRepository : IPrioritetizationParameterRepository
    {
        private readonly UserStoryContext context;
        private readonly IMapper mapper;

        public PrioritetizationParameterRepository(UserStoryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public List<PrioritetizationParameter> GetPrioritetizationParameter()
        {
            try
            {
                var obj = context.PrioritetizationParameters.ToList();
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

        public PrioritetizationParameter GetPrioritetizationParameterById(Guid prioritetId)
        {
            return context.PrioritetizationParameters.FirstOrDefault(e => e.PrioritetizationParameterId == prioritetId);
        }

        public PrioritetizationParameterConfirmation CreatePrioritetizationParameter(PrioritetizationParameter prioritetizationParameter)
        {
            var createdEntity = context.Add(prioritetizationParameter);
            return mapper.Map<PrioritetizationParameterConfirmation>(createdEntity.Entity);
        }

        public void DeletePrioritetizationParameter(Guid prioritetId)
        {
            var prioritetP = GetPrioritetizationParameterById(prioritetId);
            context.Remove(prioritetP);
        }

        public void UpdatePrioritetizationParameter(PrioritetizationParameter prioritetizationParameter)
        {

        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
