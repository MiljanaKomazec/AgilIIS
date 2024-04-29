using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sprint.Data.DataBacklog;
using Sprint.Data.DataPOBI;
using Sprint.Helper;
using Sprint.Models;
using Sprint.Models.ModelBacklog;
using Sprint.Models.ModelPOBI;
using Sprint.ServiceCalls;

namespace Sprint.Controllers
{
    [ApiController]
    [Route("api/sprint/backlogItem/backlog")]
    [Produces("application/json", "application/xml")]
    public class BacklogController : ControllerBase
    {
        private readonly IBacklogRepository backlogRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IServiceCalls serviceCalls;

        public BacklogController(IBacklogRepository backlogRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IServiceCalls serviceCalls)
        {
            this.backlogRepository = backlogRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.serviceCalls = serviceCalls;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<BacklogDTO>> GetBacklog()
        {
            List<BacklogB> backlog = backlogRepository.GetBacklog();
            if (backlog == null || backlog.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetBacklog", "List of backlogs is empty.");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetBacklog", "Backlogs successfuly restored.");
            return Ok(mapper.Map<List<BacklogDTO>>(backlog));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{backlogId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<BacklogDTO> GetBacklogById(Guid backlogId)
        {
            var backlog = backlogRepository.GetBacklogById(backlogId);

            if (backlog == null)
            {
                loggerService.Log(LogLevel.Warning, "GetBacklogById", "Backlog with id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetBacklogById", "Backlog successfuly restored.");
            return Ok(mapper.Map<BacklogDTO>(backlog));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public ActionResult<BacklogConfirmationDTO> CreateBacklog([FromBody] BacklogCreateDTO backlog)
        {


            try
            {

                BacklogB backlogModel = mapper.Map<BacklogB>(backlog);
                BacklogConfirmation confirmation = backlogRepository.CreateBacklog(backlogModel);
                backlogRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetBacklog", "BacklogB", new { backlogId = confirmation.BacklogId });
                loggerService.Log(LogLevel.Information, "CreateBacklog", "Backlog successfuly created.");
                return Ok(mapper.Map<BacklogConfirmationDTO>(confirmation));


            }
            catch
            {
                loggerService.Log(LogLevel.Error, "CreateBacklog", "An error occured while entering the backlog.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public ActionResult<BacklogDTO> UpdateBacklog(BacklogUpdateDTO backlog)
        {
            try
            {

                var oldBacklog = backlogRepository.GetBacklogById(backlog.BacklogId);
                if (oldBacklog == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateBacklog", "Backlog with id not found");
                    return NotFound();
                }
                BacklogB backlogEntity = mapper.Map<BacklogB>(backlog);

                mapper.Map(backlogEntity, oldBacklog);

                backlogRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "UpdateBacklog", "Backlog successfuly updated.");
                return Ok(mapper.Map<BacklogDTO>(oldBacklog));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Error, "UpdateBacklog", "Error occured while editing the backlog.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{backlogId}")]
        [EnableCors("AllowOrigin")]
        public IActionResult DeleteBacklog(Guid backlogId)
        {
            try
            {
                var backlog = backlogRepository.GetBacklogById(backlogId);

                if (backlog == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteBacklog", "Backlog with id not found");
                    return NotFound();
                }

                backlogRepository.DeleteBacklog(backlogId);
                backlogRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteBacklog", "Backlog successfuly deleted.");
                return NoContent();
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Error, "DeleteBacklog", "An error occured while deleting the backlog.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("userStory/{backlogId}")]
        public async Task<ActionResult<List<UserStoryDTO>>> GetUserStoriesByBacklogId(Guid backlogId)
        {
            var stories = await serviceCalls.GetUserStoriesByBacklogId(backlogId);

            if (stories == null || stories.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetUserStoriesByBacklogId", "User story with this ID does not exist");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetUserStoriesByBacklogId", "User story with this ID successfully restored");
            return Ok(stories);
        }
    }
}
