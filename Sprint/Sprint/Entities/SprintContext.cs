using Microsoft.EntityFrameworkCore;
using Sprint.Models.ModelBacklog;
using Sprint.Models.ModelBacklogItem;
using Sprint.Models.ModelPOBI;
using Sprint.Models.ModelSprint;

namespace Sprint.Entities
{
    public class SprintContext : DbContext
    {
        private readonly IConfiguration configuration;

        public SprintContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<SprintS> Sprint { get; set; }

        public DbSet<BacklogB> Backlog { get; set; }

        public DbSet<BacklogItemBI> BacklogItem { get; set; }

        public DbSet<PhaseOfBacklogItem> PhaseOfBacklogItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SprintDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SprintS>()
                .HasData(new
                {
                    SprintId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    DurationSprint = "2 weeks",
                    StartOfSprint = DateTime.Parse("2020-12-15T09:00:00"),
                    EndOfSprint = DateTime.Parse("2020-12-30T09:00:00")
                });

            builder.Entity<SprintS>()
                .HasData(new
                {
                    SprintId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    DurationSprint = "2 weeks",
                    StartOfSprint = DateTime.Parse("2020-12-15T09:00:00"),
                    EndOfSprint = DateTime.Parse("2020-12-30T09:00:00")
                });

            builder.Entity<BacklogB>()
                .HasData(new
                {
                    BacklogId = Guid.Parse("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"),
                    NameBacklog = "Project 1"
                });

            builder.Entity<BacklogB>()
                .HasData(new
                {
                    BacklogId = Guid.Parse("db7e7a04-8082-4ebb-88b0-d05f9dae4243"),
                    NameBacklog = "Project 2"
                });

            builder.Entity<BacklogItemBI>()
               .HasData(new
               {
                   BacklogItemId = Guid.Parse("45d01a65-a992-45cc-b670-1ffdd179a8f2"),
                   TimeAddedBacklogItem = "11:00 PM",
                   BacklogId = Guid.Parse("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"),
                   SprintId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   POBIId = Guid.Parse("290591c5-7054-4b03-9e40-42c5e1eadde2")

               });

            builder.Entity<BacklogItemBI>()
               .HasData(new
               {
                   BacklogItemId = Guid.Parse("6edbc9cb-32bb-48a3-90b6-fa070eede946"),
                   TimeAddedBacklogItem = "11:00 PM",
                   BacklogId = Guid.Parse("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"),
                   SprintId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   POBIId = Guid.Parse("290591c5-7054-4b03-9e40-42c5e1eadde2")
               });

            builder.Entity<PhaseOfBacklogItem>()
               .HasData(new
               {
                   POBIId = Guid.Parse("290591c5-7054-4b03-9e40-42c5e1eadde2"),
                   NameOfPOBI = "Done"
               });

            builder.Entity<PhaseOfBacklogItem>()
                .HasData(new
                {
                    POBIId = Guid.Parse("ed6e1f21-748a-4801-9a94-85e52b8fb256"),
                    NameOfPOBI = "Waiting"
                });

        }

        
    }
}
