using Grupa11_Calendar.Entities;
using Grupa11_Calendar.InterfaceRepository;
using Grupa11_Calendar.Models;

namespace Grupa11_Calendar.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly CalendarContext dbContext;
        public EventRepository(CalendarContext context)
        {
            dbContext = context;
        }
        public Event CreateEvent(Event @event)
        {
            dbContext.Events.Add(@event);
            dbContext.SaveChanges();
            return @event;
        }

        public void DeleteEvent(Guid id)
        {
            dbContext.Events.Remove(GetEventById(id));
            dbContext.SaveChanges();
        }

        public Event GetEventById(Guid id)
        {
            return dbContext.Events.FirstOrDefault(e => e.EventId == id);
        }

        public List<Event> GetEvents()
        {
            try
            {
                var obj = dbContext.Events.ToList();
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

        public Event UpdateEvent(Event @event)
        {
            Event eventUpdate = GetEventById(@event.EventId);
            eventUpdate.EventName = @event.EventName;
            eventUpdate.EventDate = @event.EventDate;
            eventUpdate.EventTime = @event.EventTime;
            eventUpdate.EventDescription = @event.EventDescription;
            eventUpdate.EventTypeId = @event.EventTypeId;
            eventUpdate.CalendarId = @event.CalendarId;
            //eventUpdate.EventType = @event.EventType;
            //eventUpdate.Calendar = @event.Calendar;
            dbContext.SaveChanges();
            return @event;
        }
        public List<Event> GetEventByEventTypeId(Guid eventTypeId)
        {
            return dbContext.Events.Where(e => e.EventTypeId == eventTypeId).ToList();
        }
        public List<Event> GetEventByCalendarId(Guid calendarId)
        {
            return dbContext.Events.Where(e => e.CalendarId == calendarId).ToList();
        }
    }
}
