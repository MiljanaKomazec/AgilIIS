namespace Sprint.Models.ModelSprint
{
    public class SprintUpdateDTO
    {
        public Guid SprintId { get; set; }
        public string DurationSprint { get; set; }
        public DateTime StartOfSprint { get; set; }
        public DateTime EndOfSprint { get; set; }
    }
}
