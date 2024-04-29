using Microsoft.EntityFrameworkCore;
using UserStory.Models.ModelFunctionality;
using UserStory.Models.ModelPP;
using UserStory.Models.ModelTask;
using UserStory.Models.ModelUserStory;

namespace UserStory.Entities
{
    public class UserStoryContext: DbContext
    {
        private readonly IConfiguration configuration;

        public UserStoryContext()
        {

        }

        public UserStoryContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<UserStoryRoot> UserStories { get; set; }
        public DbSet<TaskE> Tasks { get; set; }
        public DbSet<Functionality> Functionallities { get; set; }
        public DbSet<PrioritetizationParameter> PrioritetizationParameters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserStoryDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
       protected override void OnModelCreating(ModelBuilder builder)
        {
            //PrioritetizationParameter
            builder.Entity<PrioritetizationParameter>()
                .HasData(new
                {
                    PrioritetizationParameterId = Guid.Parse("83988e22-a297-4158-b829-ef5df2344a3f"),
                    ValueForCustomerPP = 10,
                    CostPP = 150.60m
                });

            builder.Entity<PrioritetizationParameter>()
                .HasData(new
                {
                    PrioritetizationParameterId = Guid.Parse("1c68a0db-ed8c-446d-a0ba-2f00e9df8c4c"),
                    ValueForCustomerPP = 25,
                    CostPP = 250.30m
                });

            //UserStory
            builder.Entity<UserStoryRoot>()
                .HasData(new
                {
                    UserStoryRootId = Guid.Parse("05da16d0-6c28-4206-b770-e458afd0e2d2"),
                    TextUserStory = "Kao admnistrator zelim dodati novog korisnika.",
                    PartOfEpic = "Uptavljanje korisnicima",
                    PrioritetizationParameterId = Guid.Parse("83988e22-a297-4158-b829-ef5df2344a3f"),
                    BacklogId = Guid.Parse("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc")
                    //SprintId
                });

            builder.Entity<UserStoryRoot>()
                .HasData(new
                {
                    UserStoryRootId = Guid.Parse("3d48c095-a7d0-4f13-96d7-4d694564ec1d"),
                    TextUserStory = "Kao product owner zelim pogledati predstojece sastanke.",
                    PartOfEpic = "Planiranje i organizovanje",
                    PrioritetizationParameterId = Guid.Parse("1c68a0db-ed8c-446d-a0ba-2f00e9df8c4c"),
                    BacklogId = Guid.Parse("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc")
                    //SprintId
                });

            //Functionality
            builder.Entity<Functionality>()
                .HasData(new
                {
                    FunctionalityId = Guid.Parse("28b2a55a-0f35-41b8-aca2-83a49479369f"),
                    TextFunctionality = "Dodavanje novog korisnika",
                    UserStoryRootId = Guid.Parse("05da16d0-6c28-4206-b770-e458afd0e2d2")
                    //SprintId
                });

            builder.Entity<Functionality>()
                .HasData(new
                {
                    FunctionalityId = Guid.Parse("cb553e9d-7594-485e-8449-2d8aa8b2fd68"),
                    TextFunctionality = "Pregled sastanaka",
                    UserStoryRootId = Guid.Parse("3d48c095-a7d0-4f13-96d7-4d694564ec1d")
                    //SprintId
                });


            //Task
            builder.Entity<TaskE>()
                .HasData(new
                {
                    TaskId = Guid.Parse("51ca50c4-0f5a-48fa-a3d6-84b56c392bd9"),
                    TextTask = "Validacija podataka novog korisnika",
                    FunctionalityId = Guid.Parse("28b2a55a-0f35-41b8-aca2-83a49479369f")
                    //SprintId
                });

            builder.Entity<TaskE>()
                .HasData(new
                {
                    TaskId = Guid.Parse("a869d41e-9647-49a7-9029-5a25b5ce0633"),
                    TextTask = "Implementacija dugmeta za dodavanje sprinta",
                    FunctionalityId = Guid.Parse("cb553e9d-7594-485e-8449-2d8aa8b2fd68")
                    //SprintId
                });
        }
    }

}
