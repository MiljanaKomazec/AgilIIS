using CommentService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CommentService.Entiti
{
    public class CommentContext : DbContext
    {
        private readonly IConfiguration configuration;

        public CommentContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Comment> Comment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CommentDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Comment>()
                .HasData(new
                {
                    CommentId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    DateComment = DateTime.Parse("2021-06-27T08:00:00"),
                    TextComment = "Potrebno je proširiti ovu korisničku priču.",
                    UserStoryRootId = Guid.Parse("6cf6c4c5-40bc-4c67-b2a6-5d61959d6b84"),
                    UserId = Guid.Parse("cbea5366-bf13-40ab-a518-c9b6f81bbfdf")

                });

            builder.Entity<Comment>()
                .HasData(new
                {
                    CommentId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    DateComment = DateTime.Parse("2023-11-15T14:00:00"),
                    TextComment = "Dobro napisano.",
                    UserStoryRootId = Guid.Parse("05da16d0-6c28-4206-b770-e458afd0e2d2"),
                    UserId = Guid.Parse("cbea5366-bf13-40ab-a518-c9b6f81bbfdf")
                });
        }

    }
}
