using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserStory.Data.DataPP;
using UserStory.Data.DataUserStory;
using UserStory.Helpers;
using UserStory.Models.ModelPP;
using UserStory.Models.ModelUserStory;

namespace UserStory.Controllers
{
    [ApiController]
    [Route("api/userStory/prioritetizationParameter")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class PrioritetizationParameterController : Controller
    {
        private readonly IPrioritetizationParameterRepository prioritetizationParameterRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public PrioritetizationParameterController(IPrioritetizationParameterRepository prioritetizationParameterRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.prioritetizationParameterRepository = prioritetizationParameterRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet]   
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<PrioritetizationParameterDTO>> GetPrioritetizationParameter()
        {
            List<PrioritetizationParameter> parameters = prioritetizationParameterRepository.GetPrioritetizationParameter();

            //Ukoliko nismo pronašli ni jednu prijavu vratiti status 204 (NoContent)
            if (parameters == null || parameters.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllParameterPrioritetization", "List of  parameter prioritetization is empty.");
                NoContent();
                return BadRequest("List is empty!");
            }

            loggerService.Log(LogLevel.Information, "GetAllParameterPrioritetization", "Parameter prioritetization successfully restored");
            //Ukoliko smo našli neke prijava vratiti status 200 i listu pronađenih prijava (DTO objekti)
            return Ok(mapper.Map<List<PrioritetizationParameterDTO>>(parameters));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("{prioritetizationParameterId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public ActionResult<PrioritetizationParameterDTO> GetPrioritetizationParameterById(Guid prioritetizationParameterId)
        {
            var parameter = prioritetizationParameterRepository.GetPrioritetizationParameterById(prioritetizationParameterId);

            if (parameter == null)
            {
                loggerService.Log(LogLevel.Warning, "GetPrioritetizationParameterById", $"Prioritetization parameter with ID: {prioritetizationParameterId} not found.");
                return NotFound();
            }

            loggerService.Log(LogLevel.Information, "GetPrioritetizationParameterById", $"Prioritetization parameter with ID: {prioritetizationParameterId} successfully restored");
            return Ok(mapper.Map<PrioritetizationParameterDTO>(parameter));
        }

        [HttpPost]
        [Consumes("application/json")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PrioritetizationParameterConfirmationDTO> CreatePrioritetizationParameter([FromBody] PrioritetizationParameterCreationDTO parameter)
        {
            try
            {
                PrioritetizationParameter prioritetizationParameter = mapper.Map<PrioritetizationParameter>(parameter);

                var parameterValid = ValidatePrioritetizationParameter(prioritetizationParameter);

                if (!parameterValid)
                {
                    loggerService.Log(LogLevel.Warning, "CreatePrioritetizationParameter", $"Prioritetization parameter with this ID already exist. Please enter valid ID.");
                    return BadRequest("Prioritetization parameter with this ID already exist. Please enter valid ID.");
                }

                PrioritetizationParameterConfirmation confirmation = prioritetizationParameterRepository.CreatePrioritetizationParameter(prioritetizationParameter);
                prioritetizationParameterRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetPrioritetizationParameter", "PrioritetizationParameter", new { prioritetizationParameterId = confirmation.PrioritetizationParameterId });
                loggerService.Log(LogLevel.Information, "CreatePrioritetizationParameter", $"Prioritetization parameter with values: {JsonConvert.SerializeObject(parameter)} successfully created");
                return Ok(mapper.Map<PrioritetizationParameterConfirmationDTO>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "CreatePrioritetizationParameter", $"An error occurred while entering the prioritetization parameter with values: {JsonConvert.SerializeObject(parameter)}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        private bool ValidatePrioritetizationParameter(PrioritetizationParameter pp)
        {
            List<PrioritetizationParameter> parameters = prioritetizationParameterRepository.GetPrioritetizationParameter();
            foreach (PrioritetizationParameter parameter in parameters)
            {
                if (parameter.PrioritetizationParameterId == pp.PrioritetizationParameterId)
                {
                    return false;
                }
            }
            return true;
        }

        [HttpPut]
        [Consumes("application/json")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PrioritetizationParameterDTO> UpdatePrioritetizationParameter(PrioritetizationParameterUpdateDTO prioritetizationParameter)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var existPrioritetizationParameter = prioritetizationParameterRepository.GetPrioritetizationParameterById(prioritetizationParameter.PrioritetizationParameterId);
                if (existPrioritetizationParameter == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdatePrioritetizationParameter", $"Prioritetization parameter with ID: {prioritetizationParameter.PrioritetizationParameterId} does not exist");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                PrioritetizationParameter prioritetizationParameterEntity = mapper.Map<PrioritetizationParameter>(prioritetizationParameter);

                mapper.Map(prioritetizationParameterEntity, existPrioritetizationParameter); //Update objekta koji treba da sačuvamo u bazi                

                prioritetizationParameterRepository.SaveChanges(); //Perzistiramo promene
                loggerService.Log(LogLevel.Information, "UpdatePrioritetizationParameter", $"Prioritetization parameter with ID: {prioritetizationParameter.PrioritetizationParameterId} successfully updated. The old values -> {existPrioritetizationParameter} are replaced.");
                return Ok(mapper.Map<PrioritetizationParameterDTO>(existPrioritetizationParameter));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdatePrioritetizationParameter", $"Error when editing prioritetization parameter with ID: {prioritetizationParameter.PrioritetizationParameterId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        [HttpDelete("{prioritetizationParameterId}")]
        public IActionResult DeletePrioritetizationParameter(Guid prioritetizationParameterId)
        {
            //TODO: Dodati logiku da se studentu vrate sredstva na račun ukoliko se obriše prijava ispita
            try
            {
                var parameter = prioritetizationParameterRepository.GetPrioritetizationParameterById(prioritetizationParameterId);

                if (parameter == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeletePrioritetizationParameter", $"Prioritetization parameter with ID: {prioritetizationParameterId} does not exist");
                    return NotFound();
                }

                prioritetizationParameterRepository.DeletePrioritetizationParameter(prioritetizationParameterId);
                loggerService.Log(LogLevel.Information, "DeletePrioritetizationParameter", $"Prioritetization parameter with ID: {prioritetizationParameterId} successfully deleted.");
                prioritetizationParameterRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "DeletePrioritetizationParameter", $"An error occured while deleting prioritetization parameter with ID: {prioritetizationParameterId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

       
    }
    }
