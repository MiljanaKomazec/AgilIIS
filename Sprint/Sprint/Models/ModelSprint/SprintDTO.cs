namespace Sprint.Models.ModelSprint
{
    public class SprintDTO
    {
        public Guid SprintId { get; set; }
        #region SprintDTO
        public string DurationSprint { get; set; }
        public DateTime StartOfSprint { get; set; }
        public DateTime EndOfSprint { get; set; }
        #endregion
    }
}
