namespace StoryPointAPI.Models
{
    public class StoryPoint
    {
        public Guid StoryPointId { get; set; }
        #region StoryPoint
        public int ValueStoryPoint { get; set; }
 
        public Guid? UserStoryRootId { get; set; }
        #endregion
    }
}
