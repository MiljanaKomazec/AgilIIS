using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserStory.Data.DataFunctionallity;
using UserStory.Data.DataUserStory;
using UserStory.Entities;
using UserStory.Helpers;
using UserStory.Models.ModelFunctionality;
using UserStory.Models.ModelUserStory;

namespace UserStory.Controllers
{
    [ApiController]
    [Route("api/userStory/functionality")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class FunctionalityController : Controller
    {
        private readonly IFunctionalityRepository functionalityRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public FunctionalityController( IFunctionalityRepository functionalityRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.functionalityRepository = functionalityRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet]   
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<FunctionalityDTO>>> GetFunctionality()
        {
            var functionalities = await functionalityRepository.GetFunctionality();
            if (functionalities == null || functionalities.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetAllFunctionality", "List of functionalities is empty.");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetAllFunctionality", "Functonalities successfully restored");
            return Ok(functionalities);

        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("{functionalityId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<FunctionalityDTO>> GetFunctionalityById(Guid functionalityId)
        {
            var functionality = await functionalityRepository.GetFunctionalityById(functionalityId);

            if (functionality == null)
            {
                await loggerService.Log(LogLevel.Warning, "GetFunctionalityById", $"Functionality with ID: {functionalityId} not found.");
                return NotFound();
            }

            await loggerService.Log(LogLevel.Information, "GetFunctionalityById", $"Functionality with ID: {functionalityId} successfully restored");
            return Ok(mapper.Map<FunctionalityDTO>(functionality));
        }

        [HttpPost]
        [Consumes("application/json")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FunctionalityConfirmationDTO>> CreateFunctionality([FromBody] FunctionalityCreationDTO functionality)
        {
            try
            {
                Functionality functionality1 = mapper.Map<Functionality>(functionality);
                functionality1.FunctionalityId = Guid.NewGuid();
                Console.WriteLine(functionality1);

                var functionalityValid = await ValidateFunctionality(functionality1);

                if (!functionalityValid)
                {
                    await loggerService.Log(LogLevel.Warning, "CreateFunctionality", $"Functionality with this ID already exist. Please enter valid ID.");
                    return BadRequest("Functionality with this ID already exist. Please enter valid ID.");
                }

                FunctionalityConfirmation confirmation = await functionalityRepository.CreateFunctionality(functionality1);
                await functionalityRepository.SaveChanges();
                //string location = linkGenerator.GetPathByAction("GetFunctionality", "Functionality", new { functionalityId = confirmation.FunctionalityId });
                await loggerService.Log(LogLevel.Information, "CreateFunctionality", $"Functionality with values: {JsonConvert.SerializeObject(functionality)} successfully created");
                return Ok(mapper.Map<FunctionalityConfirmationDTO>(confirmation));
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "CreateFunctionality", $"An error occurred while entering the functionality with values: {JsonConvert.SerializeObject(functionality)}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        private async Task<bool> ValidateFunctionality(Functionality functionality)
        {
            List<Functionality> functionalities = await functionalityRepository.GetFunctionality();
            foreach (Functionality func in functionalities)
            {
                if (func.FunctionalityId == functionality.FunctionalityId)
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
        public async Task<ActionResult<FunctionalityDTO>> UpdateFunctionality(FunctionalityUpdateDTO functionality)
        {
            try
            {
                //Proveriti da li postoji funkcionalnost koju pokušavamo da ažuriramo.
                var existFunctionality = await functionalityRepository.GetFunctionalityById(functionality.FunctionalityId);
                if (existFunctionality == null)
                {
                    await loggerService.Log(LogLevel.Warning, "UpdateFunctionality", $"Functionality with ID: {functionality.FunctionalityId} does not exist");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                Functionality functionalityEntity = mapper.Map<Functionality>(functionality);

                mapper.Map(functionalityEntity, existFunctionality); //Update objekta koji treba da sačuvamo u bazi                

                await functionalityRepository.SaveChanges(); //Perzistiramo promene
                await loggerService.Log(LogLevel.Information, "UpdateFunctionality", $"Functionality with ID: {functionality.FunctionalityId} successfully updated. The old values -> {existFunctionality} are replaced.");
                return Ok(mapper.Map<FunctionalityDTO>(existFunctionality));
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "UpdateFunctionality", $"Error when editing functionality with ID: {functionality.FunctionalityId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        [HttpDelete("{functionalityId}")]
        public async Task<ActionResult> DeleteFunctionality(Guid functionalityId)
        {
            try
            {
                var functionality = await functionalityRepository.GetFunctionalityById(functionalityId);

                if (functionality == null)
                {
                    await loggerService.Log(LogLevel.Warning, "DeleteFunctionality", $"Functionality with ID: {functionalityId} does not exist");
                    return NotFound();
                }

                await functionalityRepository.DeleteFunctionality(functionalityId);
                await functionalityRepository.SaveChanges();
                await loggerService.Log(LogLevel.Information, "DeleteFunctionality", $"Functionality with ID: {functionalityId} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "DeleteFunctionality", $"An error occured while deleting functionality with ID: {functionalityId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("userStory/{userStoryRootId}")]
        public async Task<ActionResult<List<FunctionalityDTO>>> GetFunctionalityByUserStory(Guid userStoryRootId)
        {
            var functionalities = await functionalityRepository.GetFunctionalityByUserStory(userStoryRootId);

            if (functionalities == null || functionalities.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetFunctionalityByUserStory", $"Functionality where user story ID: {userStoryRootId} does not exist");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetFunctionalityByUserStory", $"Functionality where user story ID: {userStoryRootId} successfully restored");
            return Ok(mapper.Map<List<FunctionalityDTO>>(functionalities));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("sprint/{sprintId}")]
        public async Task<ActionResult<List<FunctionalityDTO>>> GetFunctionalityBySprintId(Guid sprintId)
        {
            var functionalities = await functionalityRepository.GetFunctionalitiesBySprintId(sprintId);

            if (functionalities == null || functionalities.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetFunctionalityBySprintId", "Functionalities with this ID does not exist");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetFunctionalityBySprintId", "Functionalities with this ID successfully restored");
            return Ok(functionalities);
        }

    } 
}
