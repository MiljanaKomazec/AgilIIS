using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sprint.Data.DataSprint;
using Sprint.Helper;
using Sprint.Models;
using Sprint.Models.ModelSprint;
using Sprint.ServiceCalls;

namespace Sprint.Controllers
{

    [ApiController]
    [Route("api/sprint")]
    [Produces("application/json", "application/xml")] 
    public class SprintController : ControllerBase
    {
        private readonly ISprintRepository sprintRepository;
        private readonly LinkGenerator linkGenerator; 
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IServiceCalls serviceCalls;

        public SprintController(ISprintRepository sprintRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IServiceCalls serviceCalls)
        {
            this.sprintRepository = sprintRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.serviceCalls = serviceCalls;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<SprintDTO>> GetSprint()
        {
            List<SprintS> sprint = sprintRepository.GetSprint();
            if (sprint == null || sprint.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetSprint", "List of sprints is empty.");
                return NoContent();
            }

            //Ukoliko smo našli neke prijava vratiti status 200 i listu pronađenih prijava (DTO objekti)
            loggerService.Log(LogLevel.Information, "GetSprint", "Sprints successfuly restored.");
            return Ok(mapper.Map<List<SprintDTO>>(sprint));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{sprintid}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<SprintDTO> GetSprintById(Guid sprintid) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var sprint = sprintRepository.GetSprintById(sprintid);

            if (sprint == null)
            {
                loggerService.Log(LogLevel.Warning, "GetSprintById", "Sprint with id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetSprintById", "Sprint item successfuly restored.");
            return Ok(mapper.Map<SprintDTO>(sprint));
        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public ActionResult<SprintConfirmationDTO> CreateSprint([FromBody] SprintCreationDTO sprint)
        {

            
            try
            {

                SprintS sprintModel = mapper.Map<SprintS>(sprint);
                bool sprintValid = ValidateSprint(sprintModel);

                if (!sprintValid)
                {
                    loggerService.Log(LogLevel.Warning, "CreateSprint", "Sprint already exist. Please enter valid time.");
                    return BadRequest("Sprint already exist. Please enter valid time.");
                }
                SprintConfirmation confirmation = sprintRepository.CreateSprint(sprintModel);
                sprintRepository.SaveChanges(); 
                string location = linkGenerator.GetPathByAction("GetSprint", "SprintS", new {sprintid = confirmation.SprintId});
                loggerService.Log(LogLevel.Information, "CreateSprint", "Sprint successfuly created.");
                return Ok(mapper.Map<SprintConfirmationDTO>(confirmation));


            }
            catch
            {
                loggerService.Log(LogLevel.Error, "CreateSprint", "An error occured while entering the sprint.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
            
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public ActionResult<SprintDTO> UpdateSprint(SprintUpdateDTO sprint)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldSprint = sprintRepository.GetSprintById(sprint.SprintId);
                if (oldSprint == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateSprint", "Sprint with id not found");
                    return NotFound();
                }
                SprintS sprintEntity = mapper.Map<SprintS>(sprint);

                mapper.Map(sprintEntity, oldSprint); //Update objekta koji treba da sačuvamo u bazi                

                sprintRepository.SaveChanges(); //Perzistiramo promene
                loggerService.Log(LogLevel.Information, "UpdateSprint", "Sprint successfuly updated.");
                return Ok(mapper.Map<SprintDTO>(oldSprint));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Error, "UpdateSprint", "Error occured while editing the sprint.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{sprintid}")]
        [EnableCors("AllowOrigin")]
        public IActionResult DeleteSprint(Guid sprintid)
        {
            //TODO: Dodati logiku da se studentu vrate sredstva na račun ukoliko se obriše prijava ispita
            try
            {
                var sprint = sprintRepository.GetSprintById(sprintid);

                if (sprint == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteSprint", "Sprint with id not found");
                    return NotFound();
                }

                sprintRepository.DeleteSprint(sprintid);
                sprintRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteSprint", "Sprint successfuly deleted.");
                return NoContent();
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Error, "DeleteSprint", "An error occured while deleting the sprint.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("userStory/{sprintId}")]
        public async Task<ActionResult<List<UserStoryDTO>>> GetUserStoriesBySprintId(Guid sprintId)
        {
            var stories = await serviceCalls.GetUserStoriesBySprintId(sprintId);

            if (stories == null || stories.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetUserStoriesBySprintId", "User story with this ID does not exist");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetUserStoriesBySprintId", "User story with this ID successfully restored");
            return Ok(stories);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("userStory/functionality/{sprintId}")]
        public async Task<ActionResult<List<FunctionalityDTO>>> GetFunctionalityBySprintId(Guid sprintId)
        {
            var functionalities = await serviceCalls.GetFunctionalityBySprintId(sprintId);

            if (functionalities == null || functionalities.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetFunctionalityBySprintId", "Functionalities with this ID does not exist");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetFunctionalityBySprintId", "Functionalities with this ID successfully restored");
            return Ok(functionalities);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("userStory/functionality/task/{sprintId}")]
        public async Task<ActionResult<List<TaskDTO>>> GetTaskBySprintId(Guid sprintId)
        {
            var tasks = await serviceCalls.GetTaskBySprintId(sprintId);

            if (tasks == null || tasks.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetTaskBySprintId", "Tasks with this ID does not exist");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetTaskBySprintId", "Tasks with this ID successfully restored");
            return Ok(tasks);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("techincalDebt/{sprintId}")]
        public async Task<ActionResult<List<TechnicalDebtDTO>>> GetTdBySprintId(Guid sprintId)
        {
            var tds = await serviceCalls.GetTdBySprintId(sprintId);

            if (tds == null || tds.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetTdBySprintId", "TechnicalDebt with this ID does not exist");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetTdBySprintId", "Technical debt with this ID successfully restored");
            return Ok(tds);
        }


        private bool ValidateSprint(SprintS sprint)
        {
            List<SprintS> sprints = sprintRepository.GetSprint();
            foreach (SprintS s in sprints)
            {
                if (s.DurationSprint == sprint.DurationSprint && s.StartOfSprint == sprint.StartOfSprint && s.EndOfSprint == sprint.EndOfSprint)
                {
                    return false;
                }
            }
            return true;
        }



    }
}
