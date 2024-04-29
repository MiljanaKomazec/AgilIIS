using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using URIS_Grupa11.DTO;
using URIS_Grupa11.Helpers;
using URIS_Grupa11.Models;
using URIS_Grupa11.Repository;

namespace URIS_Grupa11.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamController : Controller
    {
        private readonly ITeamRepository teamRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public TeamController(ITeamRepository teamRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.teamRepository = teamRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }


        /*[HttpGet]
        public ActionResult<List<TeamDto>> GetTeams()
        {
            List<Team> teams = teamRepository.GetTeams();
            if(teams == null || teams.Count == 0)
            {
                NoContent();
                return BadRequest("List is empty!");
            }
            return Ok(mapper.Map<List<TeamDto>>(teams));
        }*/
        //Ovo je napravljeno dok nema baze, da mogu da vidim Id od timova
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<Team>> GetTeams()
        {
            List<Team> teams = teamRepository.GetTeams();
            if (teams == null || teams.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetTeams", "List of teams is empty.");
                NoContent();
                return BadRequest("List is empty!");
            }
            loggerService.Log(LogLevel.Information, "GetTeams", "Team successfully restored");
            return Ok(mapper.Map<List<Team>>(teams));
        }
        [HttpGet("{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TeamDto> GetTeamById(Guid teamId)
        {
            Team teamModel = teamRepository.GetTeamById(teamId);
            if(teamModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetTeamById", $"Team with ID: {teamId} not found.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetTeamById", $"Team with ID: {teamId} successfully restored");
            return Ok(mapper.Map<TeamDto>(teamModel));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TeamDto> CreateTeam([FromBody] TeamDto team)
        {
            try
            {
                
                Team teamModel = mapper.Map<Team>(team);
                bool teamValid = ValidateTeam(teamModel);

                if (!teamValid)
                {
                    loggerService.Log(LogLevel.Warning, "CreateTeam", $"Team with this name already exist. Please enter valid name.");
                    return BadRequest("Team with this name already exist. Please enter valid name.");
                }
                Team comfirmation = teamRepository.CreateTeam(teamModel);
                string location = linkGenerator.GetPathByAction("GetTeam", "Team", new { teamId = comfirmation.TeamId  });
                loggerService.Log(LogLevel.Information, "CreateTeam", $"Team with values: {JsonConvert.SerializeObject(team)} successfully created");
                //return Created(location, mapper.Map<TeamDto>(comfirmation));
                return Ok(mapper.Map<TeamDto>(team));
                

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }
        [HttpDelete("{teamId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTeam(Guid teamId)
        {
            try
            {
                Team teamModel = teamRepository.GetTeamById(teamId);
                if(teamModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteTeam", $"Team with ID: {teamId} does not exist");
                    return NotFound("Team with this Id doesnt exists!");
                }
                teamRepository.DeleteTeam(teamId);
                loggerService.Log(LogLevel.Information, "DeleteTeam", $"Team with ID: {teamId} successfully deleted.");
                return NoContent();
            }
            catch(Exception ex)
            {
                loggerService.Log(LogLevel.Error, "DeleteTeam", $"An error occured while deleting team with ID: {teamId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Team> UpdateTeam(Team team)
        {
            try
            {
                if(teamRepository.GetTeamById(team.TeamId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateTeam", $"Team with ID: {team.TeamId} does not exist");
                    return NotFound("Team with this id doesnt exists.Please enter valid Id.");
                }
                Team comfirmation = teamRepository.UpdateTeam(team);
                loggerService.Log(LogLevel.Information, "UpdateTeam", $"Team with ID: {team.TeamId} successfully updated.");
                return Ok(comfirmation);
            }
            catch(Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdateTeam", $"Error when editing team with ID: {team.TeamId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("user/{userId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<Team>> GetTeamByUserId(Guid userId)
        {
            var comment = teamRepository.GetTeamByUserId(userId);


            if (comment == null)
            {
                loggerService.Log(LogLevel.Warning, "GetTeamByUserId", "Team with user id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetTeamByUserId", "Team successfuly restored.");
            return Ok(mapper.Map<List<Team>>(comment));
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("calendar/{calendarId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<Team>> GetTeamByCalendarId(Guid calendarId)
        {
            var comment = teamRepository.GetTeamByCalendarId(calendarId);


            if (comment == null)
            {
                loggerService.Log(LogLevel.Warning, "GetTeamByCalendarId", "Team with calendar id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetTeamByCalendarId", "Team successfuly restored.");
            return Ok(mapper.Map<List<Team>>(comment));
        }

        //Proverava da li postoji jos neki tim sa istim imenom 
        private bool ValidateTeam(Team team)
        {
            List<Team> teams = teamRepository.GetTeams();
            foreach(Team t in teams)
            {
                if(t.TeamName == team.TeamName)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
