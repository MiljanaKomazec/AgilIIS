using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using URIS_TD.DTO;
using URIS_TD.Helpers;
using URIS_TD.InterfaceRepository;
using URIS_TD.Models;

namespace URIS_TD.Controllers
{
    [ApiController]
    [Route("/api/technicalDebt")]
    public class TechnicalDebtController : Controller
    {
        private readonly ITechnicalDebtRepository tdRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;
        public TechnicalDebtController(ITechnicalDebtRepository tdRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.tdRepository = tdRepository;
            this.linkGenerator = linkGenerator; 
            this.mapper = mapper;
            this.loggerService = loggerService; 
        }

        [HttpGet]
        public ActionResult<TechnicalDebt> GetAll() 
        {
            List<TechnicalDebt> td = tdRepository.GetAllTd();
            if ( td == null || td.Count == 0 ) 
            {
                loggerService.Log(LogLevel.Warning, "GetAll", "List of technical debts is empty.");
                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAll", "Technical debts successfully restored");
            return Ok(td);
        }

        [HttpGet("{id}")]
        public ActionResult<TechnicalDebtDto> GetTdById(Guid id)
        {
            TechnicalDebt td = tdRepository.GetTdById(id);
            if (td == null) {
                loggerService.Log(LogLevel.Warning, "GetTdById", $"Td with id : {id} does not exists.");
                return NotFound(); 
            }
            loggerService.Log(LogLevel.Information, "GetTdById", $"Td with id: {id} successfully restored");
            return Ok(mapper.Map<TechnicalDebtDto>(td));

        }

        [HttpPost]
        public ActionResult<TechnicalDebtDto> CreateTd([FromBody]TechnicalDebt technicalDebt)
        {
            try 
            {
                TechnicalDebt td = mapper.Map<TechnicalDebt>(technicalDebt);
                bool modelValid = Validate(td);
                if (!modelValid)
                {
                    loggerService.Log(LogLevel.Warning, "CreateTd", "Technical debt with this name already exists.");
                    return BadRequest("All fields are requested");
                }

                TechnicalDebt confirmation = tdRepository.AddTd(td);
                string location = linkGenerator.GetPathByAction("GetById", "TechnialDebt", new { id = confirmation.IdTd });
                loggerService.Log(LogLevel.Information, "CreateTd", $"Td with values: {JsonConvert.SerializeObject(technicalDebt)} successfully created.");
                return Ok(mapper.Map<TechnicalDebtDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }

        [HttpPut]
        public ActionResult<TechnicalDebt> UpdateTd(TechnicalDebt td)
        {
            try
            {
                if(tdRepository.GetTdById(td.IdTd) == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateTd", $"Technical debt with this id : {td.IdTd} does not exists.");
                    return NotFound("Enter valid ID");
                }

                TechnicalDebt confirm = tdRepository.UpdateTd(td);
                loggerService.Log(LogLevel.Information, "UpdateTd", $"Td with id: {td.IdTd} successfully updated");
                return Ok(confirm);
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdateTd", $"An error occured while editing td with ID: {td.IdTd}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTd(Guid id)
        {
            try
            {
                TechnicalDebt td = tdRepository.GetTdById(id);
                if (td == null) {
                    loggerService.Log(LogLevel.Warning, "DeleteTd", $"Td with ID: {id} does not exist");
                    return NotFound("TD with this ID, doesnt exist"); }

                tdRepository.DeleteTd(id);
                loggerService.Log(LogLevel.Information, "DeleteTd", $"Td with id: {id} successfully deleted");
                return NoContent();

            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "DeleteTd", $"An error occured while deleting technical debt with ID: {id}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("sprint/{sprintId}")]
        public ActionResult<List<TechnicalDebtDto>> GetTdBySprintId(Guid sprintId)
        {
            var td = tdRepository.GetTdBySprintId(sprintId);


            if (td == null)
            {
                loggerService.Log(LogLevel.Warning, "GetTdBySprintId", "Technical debt with sprint id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetTdBySprintId", "Technical debt successfuly restored.");
            return Ok(mapper.Map<List<TechnicalDebtDto>>(td));
        }

        private bool Validate(TechnicalDebt td) 
        {
            if(string.IsNullOrWhiteSpace(td.NameTd)) { return false; }
            return true;
        }

    }
}
