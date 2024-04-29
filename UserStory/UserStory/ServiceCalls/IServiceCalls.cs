using StoryPointAPI.DTO;

namespace UserStory.ServiceCalls
{
    public interface IServiceCalls
    {
        //veza sa Comment
        public Task<List<CommentDTO>> GetCommentByUserStoryId(Guid userStoryRootId);

        //veza sa StoryPoint
        public Task<List<StoryPointDTO>> GetStoryPointsByUserStoryId(Guid userStoryRootId);

    }
}
