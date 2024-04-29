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
    [Route("api/technicalDebt/typeOfTechnicalDebt")]
    public class TypeOfTechnicalDebtController : Controller
    {
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ITypeOfTechnicalDebtRepository repo;
        private readonly ILoggerService loggerService;
        public TypeOfTechnicalDebtController(IMapper mapper, LinkGenerator linkGenerator, ITypeOfTechnicalDebtRepository repo, ILoggerService loggerService) 
        {
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.repo = repo;   
            this.loggerService = loggerService;
        }

        [HttpGet]
        public ActionResult<TypeOfTechnicalDebt> GetAll()
        {
            List<TypeOfTechnicalDebt> td = repo.GetAllTypesOfTd();
            if (td == null || td.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAll", "List of types of technical debts is empty.");
                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAll", "Types of Technical debts successfully restored");
            return Ok(td);
        }

        [HttpGet("{id}")]
        public ActionResult<TypeOfTechnicalDebt> GetTypeById(Guid id)
        {
            TypeOfTechnicalDebt td = repo.GetTypeOfTechnicalDebtById(id);
            if (td == null) {
                loggerService.Log(LogLevel.Warning, "GetTypeById", $"Type of td with id : {id} does not exists.");
                return NotFound(); }
            loggerService.Log(LogLevel.Information, "GetTypeById", $"Type of td with id: {id} successfully restored");
            return Ok(td);

        }

        [HttpPost]
        public ActionResult<TypeOfTechnicalDebt> CreateType([FromBody] TypeOfTechnicalDebt typeOfTd)
        {
            try
            {
                
                bool modelValid = Validate(typeOfTd);
                if (!modelValid)
                {
                    loggerService.Log(LogLevel.Warning, "CreateType", "Type of technical debt with this name already exists.");
                    return BadRequest("All fields are requested");
                }

                TypeOfTechnicalDebt confirmation = repo.AddTypeOfTd(typeOfTd);
                string location = linkGenerator.GetPathByAction("GetById", "TypeOfTechnialDebt", new { id = confirmation.IdTod });
                loggerService.Log(LogLevel.Information, "CreateType", $"Type of td with values: {JsonConvert.SerializeObject(typeOfTd)} successfully created.");
                return Ok(confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }

        }


        [HttpPut]
        public ActionResult<TypeOfTechnicalDebt> UpdateType(TypeOfTechnicalDebt td)
        {
            try
            {
                if (repo.GetTypeOfTechnicalDebtById(td.IdTod) == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateType", $"Type of technical debt with this id : {td.IdTod} does not exists.");
                    return NotFound("Enter valid ID");
                }

                TypeOfTechnicalDebt confirm = repo.UpdateTypeOfTd(td);
                loggerService.Log(LogLevel.Information, "UpdateType", $"Type of td with id: {td.IdTod} successfully updated");
                return Ok(confirm);
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdateType", $"An error occured while editing type of td with ID: {td.IdTod}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteType(Guid id)
        {
            try
            {
                TypeOfTechnicalDebt td = repo.GetTypeOfTechnicalDebtById(id);
                if (td == null) {
                    loggerService.Log(LogLevel.Warning, "DeleteType", $"Type of td with ID: {id} does not exist");
                    return NotFound("Type with this ID, doesnt exist"); }

                repo.DeleteTypeOfTechnicalDebtById(id);
                loggerService.Log(LogLevel.Information, "DeleteType", $"Type of td with id: {id} successfully deleted");
                return NoContent();

            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "DeleteType", $"An error occured while deleting type of technical debt with ID: {id}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        private bool Validate(TypeOfTechnicalDebt td)
        {
            if (string.IsNullOrWhiteSpace(td.NameTotd)) { return false; }
            return true;
        }

    }
}
