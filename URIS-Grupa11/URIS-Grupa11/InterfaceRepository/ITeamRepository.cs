using URIS_Grupa11.Models;

namespace URIS_Grupa11.Repository
{
    public interface ITeamRepository
    {
        List<Team> GetTeams();
        Team GetTeamById(Guid id);
        Team CreateTeam(Team team);
        Team UpdateTeam(Team team);
        void DeleteTeam(Guid id);
        List<Team> GetTeamByUserId(Guid id);
        List<Team> GetTeamByCalendarId(Guid id);
    }
}
