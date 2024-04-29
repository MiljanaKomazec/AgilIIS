using Grupa11_Calendar.Models;

namespace Grupa11_Calendar.InterfaceRepository
{
    public interface IEventTypeRepository
    {
        List<EventType> GetEventTypes();
        EventType GetEventTypeById(Guid id);
        EventType CreateEventType(EventType eventType);
        EventType UpdateEventType(EventType eventType);
        void DeleteEventType(Guid eventTypeId);
    }
}
