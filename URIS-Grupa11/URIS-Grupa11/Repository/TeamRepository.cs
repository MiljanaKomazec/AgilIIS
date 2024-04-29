using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using URIS_Grupa11.Entities;
using URIS_Grupa11.Models;

namespace URIS_Grupa11.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TeamContext dbContext;
        
        public TeamRepository(TeamContext context) 
        {
            dbContext = context;
        }


        public Team CreateTeam(Team team)
        {

            dbContext.Teams.Add(team);
            dbContext.SaveChanges();
            return team;

        }

        public void DeleteTeam(Guid id)
        {
            dbContext.Teams.Remove(GetTeamById(id));
            dbContext.SaveChanges();
            //Teams.Remove(GetTeamById(id));
        }

        public Team GetTeamById(Guid id)
        {
            return dbContext.Teams.FirstOrDefault(e => e.TeamId == id);
            //return Teams.FirstOrDefault(e => e.TeamId == id);
        }

        public List<Team> GetTeams()
        {
            //return Teams;
            try
            {
                var obj = dbContext.Teams.ToList();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Team UpdateTeam(Team team)
        {

            //dbContext.Teams.Update(GetTeamById(team.TeamId));
            Team teamUpdate = GetTeamById(team.TeamId);
            teamUpdate.TeamName = team.TeamName;
            teamUpdate.TeamDescription = team.TeamDescription;
            //dodato zbog stranih kljuceva kad je azurirana baza
            teamUpdate.UserId = team.UserId;
            teamUpdate.CalendarId = team.CalendarId;
            dbContext.SaveChanges();
            return team;

        }
        public List<Team> GetTeamByUserId(Guid userId)
        {
            return dbContext.Teams.Where(e => e.UserId == userId).ToList();
        }
        public List<Team> GetTeamByCalendarId(Guid calendarId)
        {
            return dbContext.Teams.Where(e => e.CalendarId == calendarId).ToList();
        }
    }
}
