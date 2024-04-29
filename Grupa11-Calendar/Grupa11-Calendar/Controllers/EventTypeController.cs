using AutoMapper;
using Grupa11_Calendar.DTO;
using Grupa11_Calendar.Helpers;
using Grupa11_Calendar.InterfaceRepository;
using Grupa11_Calendar.Models;
using Grupa11_Calendar.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Grupa11_Calendar.Controllers
{
    [ApiController]
    [Route("api/event/eventTypes")]
    public class EventTypeController : Controller
    {
        private readonly IEventTypeRepository eventTypeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public EventTypeController(IEventTypeRepository eventTypeRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.eventTypeRepository = eventTypeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<EventType>> GetEventTypes()
        {
            List<EventType> eventTypes = eventTypeRepository.GetEventTypes();
            if (eventTypes == null || eventTypes.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetEventTypes", "List of event types is empty.");
                NoContent();
                return BadRequest("List is empty!");
            }
            loggerService.Log(LogLevel.Information, "GetEventTypes", "Event types successfully restored");
            return Ok(mapper.Map<List<EventType>>(eventTypes));
        }
        [HttpGet("{eventTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EventTypeDTO> GetEventTypeById(Guid eventTypeId)
        {
            EventType eventTypeModel = eventTypeRepository.GetEventTypeById(eventTypeId);
            if (eventTypeModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetEventTypeById", $"Event type with ID: {eventTypeId} not found.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetEventTypeById", $"Event type with ID: {eventTypeId} successfully restored");
            return Ok(mapper.Map<EventTypeDTO>(eventTypeModel));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EventTypeDTO> CreateEventType([FromBody] EventTypeDTO eventType)
        {
            try
            {

                EventType eventTypeModel = mapper.Map<EventType>(eventType);
                bool eventTypeValid = ValidateEventType(eventTypeModel);

                if (!eventTypeValid)
                {
                    loggerService.Log(LogLevel.Warning, "CreateEventType", $"Event type with this name already exist. Please enter valid name.");
                    return BadRequest("EventType with this name already exist. Please enter valid name.");
                }
                EventType comfirmation = eventTypeRepository.CreateEventType(eventTypeModel);
                string location = linkGenerator.GetPathByAction("GetEventType", "EventType", new { eventTypeId = comfirmation.EventTypeId });
                loggerService.Log(LogLevel.Information, "CreateEventType", $"Event type with values: {JsonConvert.SerializeObject(eventType)} successfully created");
                //return Created(location, mapper.Map<TeamDto>(comfirmation));
                return Ok(mapper.Map<EventTypeDTO>(eventType));


            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }
        [HttpDelete("{eventTypeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteEventType(Guid eventTypeId)
        {
            try
            {
                EventType eventTypeModel = eventTypeRepository.GetEventTypeById(eventTypeId);
                if (eventTypeModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteEventType", $"Event type with ID: {eventTypeId} does not exist");
                    return NotFound("EventType with this Id doesnt exists!");
                }
                eventTypeRepository.DeleteEventType(eventTypeId);
                loggerService.Log(LogLevel.Information, "DeleteEventType", $"Event type with ID: {eventTypeId} successfully deleted.");
                return NoContent();
            }
            catch(Exception ex)
            {
                loggerService.Log(LogLevel.Error, "DeleteEventType", $"An error occured while deleting event type with ID: {eventTypeId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EventType> UpdateEventType(EventType eventType)
        {
            try
            {
                if (eventTypeRepository.GetEventTypeById(eventType.EventTypeId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateEventType", $"Event type with ID: {eventType.EventTypeId} does not exist");
                    return NotFound("EventType with this id doesnt exists.Please enter valid Id.");
                }
                EventType comfirmation = eventTypeRepository.UpdateEventType(eventType);
                loggerService.Log(LogLevel.Information, "UpdateEventType", $"Event type with ID: {eventType.EventTypeId} successfully updated.");
                return Ok(comfirmation);
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdateEventType", $"Error when editing event type with ID: {eventType.EventTypeId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        private bool ValidateEventType(EventType eventType)
        {
            List<EventType> eventTypes = eventTypeRepository.GetEventTypes();
            foreach (EventType et in eventTypes)
            {
                if (et.EventTypeName == eventType.EventTypeName)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
