using Grupa11_Calendar.Models;

namespace Grupa11_Calendar.InterfaceRepository
{
    public interface IEventRepository
    {
        List<Event> GetEvents();
        Event GetEventById(Guid id);
        Event CreateEvent(Event @event); 
        Event UpdateEvent(Event @event);
        void DeleteEvent(Guid id);
        List<Event> GetEventByEventTypeId(Guid eventTypeId);
        List<Event> GetEventByCalendarId(Guid calendarId);
    }
}
