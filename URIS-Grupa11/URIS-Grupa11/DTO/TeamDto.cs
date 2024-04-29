namespace URIS_Grupa11.DTO
{
    public class TeamDto
    {
        #region Team 
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public Guid UserId { get; set; }
        public Guid CalendarId { get; set; }
        #endregion
    }
}
