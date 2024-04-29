using Grupa11_Calendar.Entities;
using Grupa11_Calendar.InterfaceRepository;
using Grupa11_Calendar.Models;

namespace Grupa11_Calendar.Repository
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private readonly CalendarContext dbContext;


        public EventTypeRepository(CalendarContext context)
        {
            dbContext = context;
        }
        public EventType CreateEventType(EventType eventType)
        {
            dbContext.EventTypes.Add(eventType);
            dbContext.SaveChanges();
            return eventType;
        }

        public void DeleteEventType(Guid eventTypeId)
        {
            dbContext.EventTypes.Remove(GetEventTypeById(eventTypeId));
            dbContext.SaveChanges();
        }

        public EventType GetEventTypeById(Guid id)
        {
            return dbContext.EventTypes.FirstOrDefault(e => e.EventTypeId == id);
        }

        public List<EventType> GetEventTypes()
        {
            try
            {
                var obj = dbContext.EventTypes.ToList();
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

        public EventType UpdateEventType(EventType eventType)
        {
            EventType eventTypeUpdate = GetEventTypeById(eventType.EventTypeId);
            eventTypeUpdate.EventTypeName = eventType.EventTypeName;
            dbContext.SaveChanges();
            return eventType;
        }
    }
}
