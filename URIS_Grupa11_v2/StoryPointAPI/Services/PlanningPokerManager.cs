using StoryPointAPI.Models;

namespace StoryPointAPI.Services
{
    public class PlanningPokerManager
    {
        public LevelOfComplexity CalculateFinalLOC(List<int> firstHandVotes, List<int> secondHandVotes)
        {
            int mostVotedFirstHand = CalculateMostVoted(firstHandVotes);
            int mostVotedSecondHand = CalculateMostVoted(secondHandVotes);
            int finalLoc = 0;

            if (mostVotedFirstHand != 0 || mostVotedSecondHand != 0)
            {
                if (mostVotedFirstHand != mostVotedSecondHand)
                {
                    finalLoc = mostVotedFirstHand > mostVotedSecondHand
                        ? mostVotedFirstHand
                        : mostVotedSecondHand;
                }
                else
                    finalLoc = mostVotedFirstHand;
            }
            else
                finalLoc = 0;


            //Console.WriteLine($"Most Voted First Hand: {mostVotedFirstHand}, Most Voted Second Hand: {mostVotedSecondHand}, Final LOC: {finalLoc}");

            return new LevelOfComplexity
            {
                FirstHandLOC = mostVotedFirstHand,
                SecondHandLOC = mostVotedSecondHand,
                FinalLOC = finalLoc
            };
        }

        private int CalculateMostVoted(List<int> votes)
        {
            var voteCounts = votes.GroupBy(v => v)
                                  .ToDictionary(group => group.Key, group => group.Count());

            // Sortiraj po broju glasova (keyed)
            var orderedVotes = voteCounts.OrderByDescending(kv => kv.Value).ToList();

            if (orderedVotes.Count == 0)
            {
                // Ako nema glasova vrati 0 (0 vraca status poruku za reset glasanja)
                return 0;
            }

            // Ako postoje glasovi i isto puta se ponavljaju , nema pobednika dakle reset
            if (orderedVotes.Count > 1 && orderedVotes[0].Value == orderedVotes[1].Value)
            {            
                return 0;
            }

            
            return orderedVotes[0].Key;
        }
    }
}
