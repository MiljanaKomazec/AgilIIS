using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using StoryPointAPI.DTO;
using UserStory.Data.DataUserStory;
using UserStory.Helpers;
using UserStory.Models.ModelUserStory;
using UserStory.ServiceCalls;

namespace UserStory.Controllers
{
    [ApiController]
    [Route("api/userStory")]
    [Produces("application/json", "application/xml")] 
    public class UserStoryController : ControllerBase
    {
        private readonly IUserStoryRepository userStoryRepository;
        private readonly LinkGenerator linkGenerator; 
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;
        private readonly IServiceCalls serviceCalls;
        public UserStoryController (IUserStoryRepository userStoryRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService, IServiceCalls serviceCalls)
        {
            this.userStoryRepository = userStoryRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.serviceCalls = serviceCalls;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<UserStoryDTO>>> GetUserStory()
        {
            var userStories = await userStoryRepository.GetUserStory();
            if (userStories == null || userStories.Count == 0) 
            {
                await loggerService.Log(LogLevel.Warning, "GetAllUserStory", "List of user stories is empty.");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetAllUserStory", "User stories successfully restored");
            return Ok(userStories);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("{userStoryRootId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<UserStoryDTO>> GetUserStoryById(Guid userStoryRootId) 
        {
            var userStory = await userStoryRepository.GetUserStoryById(userStoryRootId);

            if (userStory == null)
            {
                await loggerService.Log(LogLevel.Warning, "GetUserStoryById", $"User story with ID: { userStoryRootId} not found.");
                return NotFound();
            }

            await loggerService.Log(LogLevel.Information, "GetUserStoryById", $"User story with ID: { userStoryRootId} successfully restored");
            return Ok(mapper.Map<UserStoryDTO>(userStory));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<UserStoryConfirmationDTO>> CreateUserStory([FromBody] UserStoryCreationDTO story)
        {
            try
            {
                UserStoryRoot userStory = mapper.Map<UserStoryRoot>(story);
                userStory.UserStoryRootId = Guid.NewGuid();
                Console.WriteLine(userStory);

                var userStoryValid =await ValidateUserStory(userStory);

                if (!userStoryValid)
                {
                    await loggerService.Log(LogLevel.Warning, "CreateUserStory", $"UserStory with this ID already exist. Please enter valid ID.");
                    return BadRequest("UserStory with this ID already exist. Please enter valid ID.");
                }

                UserStoryConfirmation confirmation = await userStoryRepository.CreateUserStory(userStory);
                await userStoryRepository.SaveChanges();
                //string location = linkGenerator.GetPathByAction("GetUserStory", "UserStoryRoot", new { userStoryRootId = confirmation.UserStoryRootId });
                await loggerService.Log(LogLevel.Information, "CreateUserStory", $"User story with values: {JsonConvert.SerializeObject(story)} successfully created");
                return Ok(mapper.Map<UserStoryConfirmationDTO>(confirmation));
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "CreateUserStory", $"An error occurred while entering the user story with values: {JsonConvert.SerializeObject(story)}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }   

       private async Task<bool> ValidateUserStory(UserStoryRoot userStory)
        {
            List<UserStoryRoot> userStories = await userStoryRepository.GetUserStory();
            foreach (UserStoryRoot story in userStories)
            {
                if (story.UserStoryRootId == userStory.UserStoryRootId)
                {
                    return false;
                }
            }
            return true;
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<UserStoryDTO>> UpdateUserStory(UserStoryUpdateDTO userStory)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldUserStory = await userStoryRepository.GetUserStoryById(userStory.UserStoryRootId);
                if (oldUserStory == null)
                {

                    await loggerService.Log(LogLevel.Warning, "UpdateUserStory", $"User story with ID: {userStory.UserStoryRootId} does not exist");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                UserStoryRoot userStoryEntity = mapper.Map < UserStoryRoot>(userStory);

                mapper.Map(userStoryEntity, oldUserStory);

                await userStoryRepository.SaveChanges();
                await loggerService.Log(LogLevel.Information, "UpdateUserStory", $"User story with ID: {userStory.UserStoryRootId} successfully updated. The old values -> {oldUserStory} are replaced.");
                return Ok(mapper.Map<UserStoryDTO>(oldUserStory));
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "UpdateUserStory", $"Error when editing user story with ID: {userStory.UserStoryRootId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error:" + ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        [HttpDelete("{userStoryRootId}")]
        public async Task<ActionResult> DeleteUserStory(Guid userStoryRootId)
        {
            try
            {
                var userStory = await userStoryRepository.GetUserStoryById(userStoryRootId);

                if (userStory == null)
                {
                    await loggerService.Log(LogLevel.Warning, "DeleteUserStory", $"User story with ID: {userStoryRootId} does not exist");
                    return NotFound();
                }

                await userStoryRepository.DeleteUserStory(userStoryRootId);
                await userStoryRepository.SaveChanges();
                await loggerService.Log(LogLevel.Information, "DeleteUserStory", $"User story with ID: {userStoryRootId} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "DeleteUserStory", $"An error occured while deleting user story with ID: {userStoryRootId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }


        [HttpGet("backlog/{backlogId}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<UserStoryDTO>>> GetUserStoriesByBacklogId(Guid backlogId)
        {
            var userStories = await userStoryRepository.GetUserStoriesByBacklogId(backlogId);

            if (userStories == null || userStories.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetUserStoriesByBacklogId", $"No user stories found for backlog ID: {backlogId}");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetUserStoriesByBacklogId", $"User stories for backlog ID: {backlogId} successfully retrieved.");
            return Ok(mapper.Map<List<UserStoryDTO>>(userStories));
        }



        //veze sa drugim agregatima

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("comment/{userStoryRootId}")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentByUserStoryId(Guid userStoryRootId)
        {
            var comments = await serviceCalls.GetCommentByUserStoryId(userStoryRootId);

            if (comments == null || comments.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetCommentByUserStoryId", $"Comments for this user story does not exist.");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetCommentByUserStoryId", $"Comments for this user story successfully restored");
            return Ok(comments);
        }


        [HttpGet("sprint/{sprintId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<List<UserStoryDTO>>> GetUserStoriesBySprintId(Guid sprintId)
        {
            var userStories = await userStoryRepository.GetUserStoriesBySprintId(sprintId);

            if (userStories == null || userStories.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetUserStoriesBySprintId", $"No user stories found for backlog ID: {sprintId}");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetUserStoriesBySprintId", $"User stories for backlog ID: {sprintId} successfully retrieved.");
            return Ok(mapper.Map<List<UserStoryDTO>>(userStories));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("storyPoint/{userStoryRootId}")]
        public async Task<ActionResult<List<StoryPointDTO>>> GetStoryPointsByUserStoryId(Guid userStoryRootId)
        {
            var storyPoints = await serviceCalls.GetStoryPointsByUserStoryId(userStoryRootId);

            if (storyPoints == null || storyPoints.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetStoryPointsByUserStoryId", $"Story Points for this user story does not exist.");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetStoryPointsByUserStoryId", $"Story Points for this user story successfully restored");
            return Ok(storyPoints);
        }
    }
}
