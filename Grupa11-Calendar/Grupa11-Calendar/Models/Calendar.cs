namespace Grupa11_Calendar.Models
{
    public class Calendar
    {
        public Guid CalendarId { get; set; }
        #region Calendar
        public string CalendarName { get; set; }
        public int NumberOfDaysCalendar { get; set; }
        public int YearCalendar { get; set; }
        public int MonthCalendar { get; set; }
        #endregion
    }
}
