namespace Grupa11_Calendar.DTO
{
    public class eventDTO
    {
        #region Event 
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public string EventDescription { get; set; }
        public Guid EventTypeId { get; set; }
        public Guid CalendarId { get; set; }
        #endregion
    }
}
