using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sprint.Data.DataBacklog;
using Sprint.Data.DataBacklogItem;
using Sprint.Helper;
using Sprint.Models;
using Sprint.Models.ModelBacklog;
using Sprint.Models.ModelBacklogItem;
using Sprint.ServiceCalls;

namespace Sprint.Controllers
{
    [ApiController]
    [Route("api/sprint/backlogItem")]
    [Produces("application/json", "application/xml")]
    public class BacklogItemController : ControllerBase
    {
        private readonly IBacklogItemRepository backlogItemRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
       

        public BacklogItemController(IBacklogItemRepository backlogItemRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.backlogItemRepository = backlogItemRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;

        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<BacklogItemDTO>>> GetBacklogItem()
        {
            var backlogItem = await backlogItemRepository.GetBacklogItem();
            if (backlogItem == null || backlogItem.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetBacklogItem", "List of backlog items is empty.");
                return NoContent();
            }
            await loggerService.Log(LogLevel.Information, "GetBacklogItem", "Backlog items successfuly restored.");
            return Ok(mapper.Map<List<BacklogItemDTO>>(backlogItem));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{backlogItemId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<BacklogItemDTO>> GetBacklogItemById(Guid backlogItemId)
        {
            var backlogItem = await backlogItemRepository.GetBacklogItemById(backlogItemId);

            if (backlogItem == null)
            {
                await loggerService.Log(LogLevel.Warning, "GetBacklogItemById", "Backlog Item with id not found");
                return NotFound();
            }
            await loggerService.Log(LogLevel.Information, "GetBacklogItemById", "Backlog item successfuly restored.");
            return Ok(mapper.Map<BacklogItemDTO>(backlogItem));
        }

        
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<BacklogItemConfirmationDTO>> CreateBacklogItem([FromBody] BacklogItemCreateDTO backlogItem)
        {


            try
            {

                BacklogItemBI backlogItemModel = mapper.Map<BacklogItemBI>(backlogItem);
                BacklogItemConfirmation confirmation = await backlogItemRepository.CreateBacklogItem(backlogItemModel);
                await backlogItemRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetBacklogItem", "BacklogItemBI", new { backlogItemId = confirmation.BacklogItemId });
                await loggerService.Log(LogLevel.Information, "CreateBacklogItem", "Backlog item successfuly created.");
                return Ok(mapper.Map<BacklogItemConfirmationDTO>(confirmation));


            }
            catch
            {
                await loggerService.Log(LogLevel.Error, "CreateBacklogItem", "An error occured while entering the backlog item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }
        

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<BacklogItemDTO>> UpdateBacklogItem(BacklogItemUpdateDTO backlogItem)
        {
            try
            {
               
                var oldBacklogItem = await backlogItemRepository.GetBacklogItemById(backlogItem.BacklogItemId);
                if (oldBacklogItem == null)
                {
                    await loggerService.Log(LogLevel.Warning, "UpdateBacklogItem", "Backlog Item with id not found");
                    return NotFound(); 
                }

                BacklogItemBI backlogItemEntity = mapper.Map<BacklogItemBI>(backlogItem);

                mapper.Map(backlogItemEntity, oldBacklogItem);

                await backlogItemRepository.SaveChanges();
                await loggerService.Log(LogLevel.Information, "UpdateBacklogItem", "Backlog item successfuly updated.");
                return Ok(mapper.Map<BacklogItemDTO>(oldBacklogItem));
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "UpdateBacklogItem", "Error occured while editing the backlog item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error:" + ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{backlogItemId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult> DeleteBacklogItem(Guid backlogItemId)
        {
            try
            {
                var backlogItem = await backlogItemRepository.GetBacklogItemById(backlogItemId);

                if (backlogItem == null)
                {
                    await loggerService.Log(LogLevel.Warning, "DeleteBacklogItem", "Backlog Item with id not found");
                    return NotFound();
                }

                await backlogItemRepository.DeleteBacklogItem(backlogItemId);
                await backlogItemRepository.SaveChanges();
                await loggerService.Log(LogLevel.Information, "DeleteBacklogItem", "Backlog item successfuly deleted.");
                return NoContent();
            }
            catch (Exception)
            {
                await loggerService.Log(LogLevel.Error, "DeleteBacklogItem", "An error occured while deleting the backlog item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("sprint/{sprintId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<BacklogItemDTO>>> GetBacklogItemBySprintId(Guid sprintId)
        {
            var backlogItem = await backlogItemRepository.GetBacklogItemBySprintId(sprintId);
            

            if (backlogItem == null || backlogItem.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetBacklogItemBySprintId", "List of backlog items is empty.");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetBacklogItemBySprintId", "Backlog items successfuly restored.");
            return Ok(mapper.Map<List<BacklogItemDTO>>(backlogItem));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("p/{pobiId}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<BacklogItemDTO>>> GetBacklogItemByPOBIId(Guid pobiId)
        {
            var backlogItem = await backlogItemRepository.GetBacklogItemByPOBIId(pobiId);

            if (backlogItem == null || backlogItem.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetBacklogItemByPOBIId", "List of backlog items is empty.");
                return NoContent();
            }



            await loggerService.Log(LogLevel.Information, "GetBacklogItemByPOBIId", "Backlog items successfuly restored.");
            return Ok(mapper.Map<List<BacklogItemDTO>>(backlogItem));
        }



    }
}
