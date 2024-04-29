using System.ComponentModel.DataAnnotations.Schema;

namespace Grupa11_Calendar.Models
{
    public class Event
    {
        public Guid EventId { get; set; }
        #region Event 
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public string EventDescription { get; set; }
        public Guid EventTypeId { get; set; }
        [ForeignKey("EventTypeId")]
        public EventType EventType { get; set; }
        public Guid CalendarId { get; set; }
        [ForeignKey("CalendarId")]
        public Calendar Calendar { get; set; }  
        #endregion
    }
}
