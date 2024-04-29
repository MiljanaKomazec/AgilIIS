using Grupa11_Calendar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Grupa11_Calendar.Entities
{
    public class CalendarContext : DbContext
    {
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }   
        //sa ovim popunjavas svoju bazu sa podacima!!!
        private readonly IConfiguration _configuration;

        public CalendarContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CalendarDB")); //OVO IZMENI NA JOS 1 MESTU
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calendar>().HasData(new
            {
                CalendarId = Guid.Parse("550e8400-e29b-41d4-a716-446655440006"),
                CalendarName = "Timski kalendar 2",
                NumberOfDaysCalendar = 12,
                YearCalendar = 2013,
                MonthCalendar = 1
            });
            modelBuilder.Entity<Calendar>().HasData(new
            {
                CalendarId = Guid.Parse("550e8400-e29b-41d4-a716-446655440007"),
                CalendarName = "Kalendar TimLidera",
                NumberOfDaysCalendar = 2,
                YearCalendar = 2022,
                MonthCalendar = 5
            });
            modelBuilder.Entity<EventType>().HasData(new
            {
                EventTypeId = Guid.Parse("550e8400-e29b-41d4-a716-446655440002"),
                EventTypeName = "Sastanak",
            });
            modelBuilder.Entity<EventType>().HasData(new
            {
                EventTypeId = Guid.Parse("550e8400-e29b-41d4-a716-446655440003"),
                EventTypeName = "Prezentacija",
            });
            modelBuilder.Entity<Event>().HasData(new
            {
                EventId = Guid.Parse("550e8400-e29b-41d4-a716-446655440004"),
                EventName = "Sastanak sa ScrumMasterom Projekta 3",
                EventDate = DateTime.Parse("2023-09-26 00:00:00"),
                EventTime = DateTime.Parse("2024-09-26 14:30:00"),
                EventDescription = "Description",
                EventTypeId = Guid.Parse("550e8400-e29b-41d4-a716-446655440003"),
                CalendarId = Guid.Parse("550e8400-e29b-41d4-a716-446655440006")
            });
            modelBuilder.Entity<Event>().HasData(new
            {
                EventId = Guid.Parse("550e8400-e29b-41d4-a716-446655440005"),
                EventName = "Upoznavanje sa novim partnerom",
                EventDate = DateTime.Parse("2024-01-10 00:00:00"),
                EventTime = DateTime.Parse("2024-01-10 15:00:00"),
                EventDescription = "Description",
                EventTypeId = Guid.Parse("550e8400-e29b-41d4-a716-446655440003"),
                CalendarId = Guid.Parse("550e8400-e29b-41d4-a716-446655440006")
            });
        }
    }
}
