using Grupa11_Calendar.Models;

namespace Grupa11_Calendar.InterfaceRepository
{
    public interface ICalendarRepository
    {
        List<Calendar> GetCalendars();
        Calendar GetCalendarById(Guid id);
        Calendar CreateCalendar(Calendar calendar);
        Calendar UpdateCalendar(Calendar calendar);
        void DeleteCalendar(Guid id);
    }
}
