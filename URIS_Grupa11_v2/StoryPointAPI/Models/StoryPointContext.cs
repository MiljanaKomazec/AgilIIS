using Microsoft.EntityFrameworkCore;

namespace StoryPointAPI.Models
{
    public class StoryPointContext : DbContext
    {
        public StoryPointContext(DbContextOptions<StoryPointContext> options)
        : base(options)
        {
        }

        public DbSet<StoryPoint> StoryPoints { get; set; } = null!;
    }
}
