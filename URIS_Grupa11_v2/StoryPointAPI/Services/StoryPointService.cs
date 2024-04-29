using StoryPointAPI.DTO;
using StoryPointAPI.Models;

namespace StoryPointAPI.Services
{
    public class StoryPointService
    {
        private readonly PlanningPokerManager _planningPokerManager;
        private readonly StoryPointContext context;

        public StoryPointService(PlanningPokerManager planningPokerManager, StoryPointContext context)
        {
            _planningPokerManager = planningPokerManager;
            this.context = context;
        }

        public void UpdateStoryPointWithFinalLOC(StoryPoint storyPoint, List<int> firstHandVotes, List<int> secondHandVotes)
        {
            LevelOfComplexity finalLOC = _planningPokerManager.CalculateFinalLOC(firstHandVotes, secondHandVotes);

            // Update StoryPoint with final Level of Complexity
            storyPoint.ValueStoryPoint = finalLOC.FinalLOC;

            // Save changes to the database or perform any necessary actions
            // dbContext.SaveChanges(); // Assuming you are using Entity Framework
        }

        public List<StoryPoint> GetStoryPointsByUserStoryId(Guid userStoryRootId) {
            return context.StoryPoints.Where(e => e.UserStoryRootId == userStoryRootId).ToList();
        }
    }
}
