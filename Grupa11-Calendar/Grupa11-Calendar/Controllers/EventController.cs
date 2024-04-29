using AutoMapper;
using Grupa11_Calendar.DTO;
using Grupa11_Calendar.Helpers;
using Grupa11_Calendar.InterfaceRepository;
using Grupa11_Calendar.Models;
using Grupa11_Calendar.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Grupa11_Calendar.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public EventController(IEventRepository eventRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.eventRepository = eventRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<Event>> GetEvents()
        {
            List<Event> events = eventRepository.GetEvents();
            if (events == null || events.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetEvents", "List of events is empty.");
                NoContent();
                return BadRequest("List is empty!");
            }
            loggerService.Log(LogLevel.Information, "GetEvents", "Events successfully restored");
            return Ok(mapper.Map<List<Event>>(events));
        }
        [HttpGet("{eventId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<eventDTO> GetEventById(Guid eventId)
        {
            Event eventModel = eventRepository.GetEventById(eventId);
            if (eventModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetEventById", $"Event with ID: {eventId} not found.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetEventById", $"Event with ID: {eventId} successfully restored");
            return Ok(mapper.Map<eventDTO>(eventModel));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<eventDTO> CreateEvent([FromBody] eventDTO @event)
        {
            try
            {

                Event eventModel = mapper.Map<Event>(@event);
                bool eventValid = ValidateEvent(eventModel);

                if (!eventValid)
                {
                    loggerService.Log(LogLevel.Warning, "CreateEvent", $"Event with this name already exist. Please enter valid name.");
                    return BadRequest("Event with this name already exist. Please enter valid name.");
                }
                Event comfirmation = eventRepository.CreateEvent(eventModel);
                string location = linkGenerator.GetPathByAction("GetEvent", "Event", new { eventId = comfirmation.EventId });
                loggerService.Log(LogLevel.Information, "CreateEvent", $"Event with values: {JsonConvert.SerializeObject(@event)} successfully created");
                //return Created(location, mapper.Map<TeamDto>(comfirmation));
                return Ok(mapper.Map<eventDTO>(@event));


            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }
        [HttpDelete("{eventId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteEvent(Guid eventId)
        {
            try
            {
                Event eventModel = eventRepository.GetEventById(eventId);
                if (eventModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteEvent", $"Event with ID: {eventId} does not exist");
                    return NotFound("Event with this Id doesnt exists!");
                }
                eventRepository.DeleteEvent(eventId);
                loggerService.Log(LogLevel.Information, "DeleteEvent", $"Event with ID: {eventId} successfully deleted.");
                return NoContent();
            }
            catch( Exception ex )
            {
                loggerService.Log(LogLevel.Error, "DeleteEvent", $"An error occured while deleting event with ID: {eventId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Event> UpdateEvent(Event @event)
        {
            try
            {
                if (eventRepository.GetEventById(@event.EventId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateEvent", $"Event with ID: {@event.EventId} does not exist");
                    return NotFound("Event with this id doesnt exists.Please enter valid Id.");
                }
                Event comfirmation = eventRepository.UpdateEvent(@event);
                loggerService.Log(LogLevel.Information, "UpdateEvent", $"Event with ID: {@event.EventId} successfully updated.");
                return Ok(comfirmation);
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdateEvent", $"Error when editing event with ID: {@event.EventId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("events/eventType/{eventTypeId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<Event>> GetEventByEventTypeId(Guid eventTypeId)
        {
            var comment = eventRepository.GetEventByEventTypeId(eventTypeId);


            if (comment == null)
            {
                loggerService.Log(LogLevel.Warning, "GetEventByEventTypeId", "Event with eventType id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetEventByEventTypeId", "Event successfuly restored.");
            return Ok(mapper.Map<List<Event>>(comment));
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("events/calendar/{calendarId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<Event>> GetEventByCalendarId(Guid calendarId)
        {
            var comment = eventRepository.GetEventByCalendarId(calendarId);


            if (comment == null)
            {
                loggerService.Log(LogLevel.Warning, "GetEventByCalendarId", "Event with calendar id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetEventByCalendarId", "Event successfuly restored.");
            return Ok(mapper.Map<List<Event>>(comment));
        }

        private bool ValidateEvent(Event @event)
        {
            List<Event> events = eventRepository.GetEvents();
            foreach (Event e in events)
            {
                if (e.EventName == @event.EventName)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
