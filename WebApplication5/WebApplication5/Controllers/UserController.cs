using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Newtonsoft.Json;
using WebApplication5.DTO;
using WebApplication5.Helpers;
using WebApplication5.InterfaceRepository;
using WebApplication5.Migrations;
using WebApplication5.Models;
using WebApplication5.ServiceCalls;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/user")]
    [EnableCors("AllowOrigin")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;
        private readonly PasswordHashService passwordHashService;
        private readonly IServiceCalls serviceCalls;
        public UserController(IUserRepository userRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService, PasswordHashService passwordHashService, IServiceCalls serviceCalls)
        {
            this.userRepository = userRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
            this.passwordHashService = passwordHashService;
            this.serviceCalls = serviceCalls;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        
        public ActionResult<List<User>> GetAllUsers()
        {
            var users = userRepository.GetUserRole();
            if ( users == null || users.Count == 0) 
            {
                loggerService.Log(LogLevel.Warning, "GetAllUsers", "List of users is empty.");
                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAllUsers", "Users successfully restored");
            return Ok(mapper.Map<List<UserRoleDto>>(users));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<User> GetUserById(Guid id) 
        {
            User user = userRepository.GetById(id);
            if (user == null) {

                loggerService.Log(LogLevel.Warning, "GetUserById", $"User with id: {id} is not found."); 
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetUserById", $"User with id: {id} successfully restored");
            return Ok(mapper.Map<UserDto>(user));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            try
            {
                User mappedUser = mapper.Map<User>(user);
                bool modelValid = userRepository.uniqueEmail(mappedUser.EmailUser);
                if(modelValid)
                {
                    loggerService.Log(LogLevel.Warning, "CreateUser", "User with this email already exists. Please enter valid email.");
                    return BadRequest("User with this email already exists. Please enter valid email.");
                }

                var passwordData = passwordHashService.HashPassword(mappedUser.PasswordUser);
                mappedUser.PasswordUser = passwordData.Item1; 
                mappedUser.Salt = passwordData.Item2;


                User confirmation = userRepository.AddUser(mappedUser);
                string location = linkGenerator.GetPathByAction("GetUserById", "User", new { id = confirmation.IDUser });
                loggerService.Log(LogLevel.Information, "CreateUser", $"User with values: {JsonConvert.SerializeObject(user)} successfully created.");
                //return Created(location, mapper.Map<UserDto>(confirmation));
                return Ok(mapper.Map<UserDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public ActionResult<User> UpdateUser (User user)
        {
            try 
            {
                if(userRepository.GetById(user.IDUser) == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateUser", $"User with ID: {user.IDUser} does not exist");
                    return NotFound("Enter valid ID");
                }
                else if (userRepository.uniqueEmail(user.EmailUser))
                {
                    loggerService.Log(LogLevel.Warning, "UpdateUser", $"User with Email: {user.EmailUser} already exists");
                    return BadRequest("User with this email already exists. Please enter valid email.");
                }

                User confirmation = userRepository.UpdateUser(user);
                loggerService.Log(LogLevel.Information, "UpdateUser", $"User with id: {user.IDUser} successfully updated");
                return Ok(confirmation);
            }
            catch(Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdateUser", $"An error occured while editing user with ID: {user.IDUser}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       
        public IActionResult DeleteUser(Guid id) 
        {
            try
            {
                User user = userRepository.GetById(id);
                if (user == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteUser", $"User with ID: {id} does not exist");
                    return NotFound("User with this ID doesnt exist");
                }
                userRepository.DeleteUser(id);
                loggerService.Log(LogLevel.Information, "DeleteUser", $"User with id: {id} successfully deleted");
                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "DeleteUser", $"An error occured while deleting user with ID: {id}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }


        }

        [HttpGet("roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
       
        public ActionResult<List<User>> GetAllUsersRoles()
        {
            var roles = new List<UserRoleDto>();
            roles = userRepository.GetUserRole();
            loggerService.Log(LogLevel.Information, "GetAllUsersRoles", "Users with roles successfully restored");
            return Ok(mapper.Map<List<UserRoleDto>>(roles));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("comment/{userId}")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentByUserStoryId(Guid userId)
        {
            var comments = await serviceCalls.GetCommentByUserId(userId);

            if (comments == null || comments.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetCommentByUserStoryId", $"Comments for this user does not exist.");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetCommentByUserStoryId", $"Comments for this user successfully restored");
            return Ok(comments);
        }


        /*private bool validateUser(User user)
        {
            if(string.IsNullOrWhiteSpace(user.EmailUser)) { return false; }
            if(string.IsNullOrWhiteSpace(user.NameUser)) { return false; }
            return true;
        }*/

    }
}
