namespace URIS_Grupa11.Models
{
    public class Team
    {
        public Guid TeamId { get; set; }
        #region Team 
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public Guid? UserId { get; set; }
        public Guid CalendarId { get; set; }
        #endregion

    }
}
