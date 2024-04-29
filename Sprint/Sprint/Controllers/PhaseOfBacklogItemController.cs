using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sprint.Data.DataPOBI;
using Sprint.Helper;
using Sprint.Models.ModelBacklog;
using Sprint.Models.ModelPOBI;

namespace Sprint.Controllers
{
    [ApiController]
    [Route("api/sprint/backlogItem/pobi")]
    [Produces("application/json", "application/xml")]
    public class PhaseOfBacklogItemController : ControllerBase
    {
        private readonly IPhaseOfBacklogItemRepository pobiRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public PhaseOfBacklogItemController(IPhaseOfBacklogItemRepository pobiRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.pobiRepository = pobiRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }


        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<PhaseOfBacklogItemDTO>> GetPhaseOfBacklogItem()
        {
            List<PhaseOfBacklogItem> pobi = pobiRepository.GetPhaseOfBacklogItem();
            if (pobi == null || pobi.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetPhaseOfBacklogItem", "List of phase of backlog items is empty.");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetPhaseOfBacklogItem", "Phase of backlog items successfuly restored.");
            return Ok(mapper.Map<List<PhaseOfBacklogItemDTO>>(pobi));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{pobiId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<PhaseOfBacklogItemDTO> GetPhaseOfBacklogItemById(Guid pobiId) 
        {
            var pobi = pobiRepository.GetPhaseOfBacklogItemById(pobiId);

            if (pobi == null)
            {
                loggerService.Log(LogLevel.Warning, "GetPhaseOfBacklogItemById", "Phase of backlog item with id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetPhaseOfBacklogItemById", "Phase of backlog item successfuly restored.");
            return Ok(mapper.Map<PhaseOfBacklogItemDTO>(pobi));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public ActionResult<PhaseOfBacklogItemConfirmationDTO> CreatePhaseOfBacklogItem([FromBody] PhaseOfBacklogItemCreateDTO pobi)
        {


            try
            {

                PhaseOfBacklogItem pobiModel = mapper.Map<PhaseOfBacklogItem>(pobi);
                PhaseOfBacklogItemConfirmation confirmation = pobiRepository.CreatePhaseOfBacklogItem(pobiModel);
                pobiRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetPhaseOfBacklogItem", "PhaseOfBacklogItem", new { pobiId = confirmation.POBIId });
                loggerService.Log(LogLevel.Information, "GetPhaseOfBacklogItem", "Phase of backlog item successfuly created.");
                return Ok(mapper.Map<PhaseOfBacklogItemConfirmationDTO>(confirmation));


            }
            catch
            {
                loggerService.Log(LogLevel.Error, "GetPhaseOfBacklogItem", "An error occured while entering the phase of backlog item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public ActionResult<PhaseOfBacklogItemDTO> UpdatePhaseOfBacklogItem(PhaseOfBacklogItemUpdateDTO pobi)
        {
            try
            {
                
                var oldPobi = pobiRepository.GetPhaseOfBacklogItemById(pobi.POBIId);
                if (oldPobi == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdatePhaseOfBacklogItem", "Phase of backlog Item with id not found");
                    return NotFound(); 
                }
                PhaseOfBacklogItem pobiEntity = mapper.Map<PhaseOfBacklogItem>(pobi);

                mapper.Map(pobiEntity, oldPobi);                 

                pobiRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "UpdatePhaseOfBacklogItem", "Phase of backlog Item successfuly updated.");
                return Ok(mapper.Map<PhaseOfBacklogItemDTO>(oldPobi));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Error, "UpdatePhaseOfBacklogItem", "Error occured while editing the phase of backlog item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{pobiId}")]
        [EnableCors("AllowOrigin")]
        public IActionResult DeletePhaseOfBacklogItem(Guid pobiId)
        {
            try
            {
                var pobi = pobiRepository.GetPhaseOfBacklogItemById(pobiId);

                if (pobi == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeletePhaseOfBacklogItem", "Phase of backlog Item with id not found");
                    return NotFound();
                }

                pobiRepository.DeletePhaseOfBacklogItem(pobiId);
                pobiRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeletePhaseOfBacklogItem", "Phase of backlog item successfuly deleted.");
                return NoContent();
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Error, "DeletePhaseOfBacklogItem", "An error occured while deleting the phase of backlog item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

    }
}
