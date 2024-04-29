

using Microsoft.EntityFrameworkCore;
using URIS_Grupa11.Models;

namespace URIS_Grupa11.Entities
{
    public class TeamContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        //sa ovim popunjavas svoju bazu sa podacima!!!
        private readonly IConfiguration _configuration;

        public TeamContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TeamDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(new
            {
                TeamId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
                TeamName = "Grupa11",
                TeamDescription = "Razvoj Agilisa",
                UserId = Guid.Parse("150e8400-e29b-41d4-a716-446655440000"),
                CalendarId = Guid.Parse("250e8400-e29b-41d4-a716-446655440000")
            });
            modelBuilder.Entity<Team>().HasData(new
            {
                TeamId = Guid.Parse("550e8400-e29b-41d4-a716-446655440001"),
                TeamName = "Grupa1",
                TeamDescription = "Modifikovanje web aplikacije",
                UserId = Guid.Parse("350e8400-e29b-41d4-a716-446655440000"),
                CalendarId = Guid.Parse("450e8400-e29b-41d4-a716-446655440000")
            });
        }

    }
}
