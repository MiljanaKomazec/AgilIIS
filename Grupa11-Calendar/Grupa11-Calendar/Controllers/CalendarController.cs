using AutoMapper;
using Grupa11_Calendar.DTO;
using Grupa11_Calendar.Helpers;
using Grupa11_Calendar.InterfaceRepository;
using Grupa11_Calendar.Models;
using Grupa11_Calendar.Repository;
using Grupa11_Calendar.ServiceCalls;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Grupa11_Calendar.Controllers
{
    [ApiController]
    [Route("api/event/calendars")]
    public class CalendarController : Controller
    {
        private readonly ICalendarRepository calendarRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IServiceCalls serviceCalls;

        public CalendarController(ICalendarRepository calendarRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IServiceCalls serviceCalls)
        {
            this.calendarRepository = calendarRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.serviceCalls = serviceCalls;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<Calendar>> GetCalendars()
        {
            List<Calendar> calendars = calendarRepository.GetCalendars();
            if (calendars == null || calendars.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetCalendars", "List of calendars is empty.");
                NoContent();
                return BadRequest("List is empty!");
            }
            loggerService.Log(LogLevel.Information, "GetCalendars", "Calendars successfully restored");
            return Ok(mapper.Map<List<Calendar>>(calendars));
        }
        [HttpGet("{calendarId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CalendarDTO> GetCalendarById(Guid calendarId)
        {
            Calendar calendarModel = calendarRepository.GetCalendarById(calendarId);
            if (calendarModel == null)
            {
                loggerService.Log(LogLevel.Warning, "GetCalendarById", $"Calendar with ID: {calendarId} not found.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetCalendarById", $"Calendar with ID: {calendarId} successfully restored");
            return Ok(mapper.Map<CalendarDTO>(calendarModel));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CalendarDTO> CreateCalendar([FromBody] CalendarDTO calendar)
        {
            try
            {

                Calendar calendarModel = mapper.Map<Calendar>(calendar);
                bool calendarValid = ValidateCalendar(calendarModel);

                if (!calendarValid)
                {
                    loggerService.Log(LogLevel.Warning, "CreateCalendar", $"Calendar with this name already exist. Please enter valid name.");
                    return BadRequest("Calendar with this name already exist. Please enter valid name.");
                }
                Calendar comfirmation = calendarRepository.CreateCalendar(calendarModel);
                string location = linkGenerator.GetPathByAction("GetCalendar", "Calendar", new { calendarId = comfirmation.CalendarId });
                loggerService.Log(LogLevel.Information, "CreateCalendar", $"Calendar with values: {JsonConvert.SerializeObject(calendar)} successfully created");
                //return Created(location, mapper.Map<TeamDto>(comfirmation));
                return Ok(mapper.Map<CalendarDTO>(calendar));


            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }
        [HttpDelete("{calendarId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCalendar(Guid calendarId)
        {
            try
            {
                Calendar calendarModel = calendarRepository.GetCalendarById(calendarId);
                if (calendarModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteCalendar", $"Calendar with ID: {calendarId} does not exist");
                    return NotFound("Calendar with this Id doesnt exists!");
                }
                calendarRepository.DeleteCalendar(calendarId);
                loggerService.Log(LogLevel.Information, "DeleteCalendar", $"Calendar with ID: {calendarId} successfully deleted.");
                return NoContent();
            }
            catch(Exception ex )
            {
                loggerService.Log(LogLevel.Error, "DeleteCalendar", $"An error occured while deleting calendar with ID: {calendarId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Calendar> UpdateCalendar(Calendar calendar)
        {
            try
            {
                if (calendarRepository.GetCalendarById(calendar.CalendarId) == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateCalendar", $"Calendar with ID: {calendar.CalendarId} does not exist");
                    return NotFound("Calendar with this id doesnt exists.Please enter valid Id.");
                }
                Calendar comfirmation = calendarRepository.UpdateCalendar(calendar);
                loggerService.Log(LogLevel.Information, "UpdateCalendar", $"Calendar with ID: {calendar.CalendarId} successfully updated.");
                return Ok(comfirmation);
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdateCalendar", $"Error when editing calendar with ID: {calendar.CalendarId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Calendar>>> GetCalendarByUserStoryId(Guid userId)
        {
            var comments = await serviceCalls.GetCalendarByUserId(userId);

            if (comments == null || comments.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetCalendarByUserId", $"Calendar for this user does not exist.");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetCalendarByUserId", $"Calendar for this user successfully restored");
            return Ok(comments);
        }
        private bool ValidateCalendar(Calendar calendar)
        {
            List<Calendar> calendars = calendarRepository.GetCalendars();
            foreach (Calendar c in calendars)
            {
                if (c.CalendarName == calendar.CalendarName)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
