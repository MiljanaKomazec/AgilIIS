using Grupa11_Calendar.Entities;
using Grupa11_Calendar.InterfaceRepository;
using Grupa11_Calendar.Models;
using Microsoft.Extensions.Logging;

namespace Grupa11_Calendar.Repository
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly CalendarContext dbContext;
        public CalendarRepository(CalendarContext context)
        {
            dbContext = context;
        }
        public Calendar CreateCalendar(Calendar calendar)
        {
            dbContext.Calendars.Add(calendar);
            dbContext.SaveChanges();
            return calendar;
        }

        public void DeleteCalendar(Guid id)
        {
            dbContext.Calendars.Remove(GetCalendarById(id));
            dbContext.SaveChanges();
        }

        public Calendar GetCalendarById(Guid id)
        {
            return dbContext.Calendars.FirstOrDefault(e => e.CalendarId == id);
        }

        public List<Calendar> GetCalendars()
        {
            try
            {
                var obj = dbContext.Calendars.ToList();
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

        public Calendar UpdateCalendar(Calendar calendar)
        {
            Calendar calendarUpdate = GetCalendarById(calendar.CalendarId);
            calendarUpdate.CalendarName = calendar.CalendarName;
            calendarUpdate.NumberOfDaysCalendar = calendar.NumberOfDaysCalendar;
            calendarUpdate.YearCalendar = calendar.YearCalendar;
            calendarUpdate.MonthCalendar = calendar.MonthCalendar;
            dbContext.SaveChanges();
            return calendar;
        }
    }
}
