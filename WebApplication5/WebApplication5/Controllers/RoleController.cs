using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using WebApplication5.DTO;
using WebApplication5.Helpers;
using WebApplication5.InterfaceRepository;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/user/role")]
    [EnableCors("AllowOrigin")]
    public class RoleController : Controller
    {

        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;
        //private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService logger;

        public RoleController(IRoleRepository roleRepository, ILoggerService logger, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.logger = logger;
            //this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<Role>> GetAllRoles()
        {
            List<Role> roles = roleRepository.GetAllRoles();
            if (roles == null || roles.Count == 0)
            {
                logger.Log(LogLevel.Warning, "GetAllRoles", "List of roles is empty.");
                return NoContent();
            }
            logger.Log(LogLevel.Information, "GetAllRoles", "Roles successfully restored");
            return Ok(mapper.Map<List<RoleDto>>(roles));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Role> GetRoleById(Guid id)
        {
            Role role = roleRepository.GetRoleById(id);
            if (role == null) { 
                
                logger.Log(LogLevel.Warning, "GetRoleById", $"Role with id : {id} does not exists."); 
                return NotFound();
            }
            logger.Log(LogLevel.Information, "GetRoleById", $"Role with id : {id} successfully restored");
            return Ok(mapper.Map<RoleDto>(role));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Role> AddRole([FromBody] Role role)
        {
            Role confirmation = roleRepository.AddRole(role);
            //string location = linkGenerator.GetPathByAction("GetRoleById", "Role", new { id = confirmation.IDRole });
            logger.Log(LogLevel.Information, "AddRole", $"Role with values : {JsonConvert.SerializeObject(role)} successfully created");
            return Ok(mapper.Map<RoleDto>(confirmation));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Role> UpdateRole(Role role)
        {
            try
            {
                if (roleRepository.GetRoleById(role.IDRole) == null)
                {
                    logger.Log(LogLevel.Warning, "UpdateRole", $"Role with this id: {role.IDRole} does not exists.");
                    return NotFound("Enter valid ID");
                }

                Role confirmation = roleRepository.UpdateRole(role);
                logger.Log(LogLevel.Information, "UpdateRole", $"Role with id : {role.IDRole} successfully updated");
                return Ok(confirmation);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "UpdateRole", $"Error whit editing role with ID: {role.IDRole}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteRole(Guid id)
        {
            try
            {
                Role role = roleRepository.GetRoleById(id);
                if (role == null)
                {
                    logger.Log(LogLevel.Warning, "DeleteRole", $"Role with this id: {id} does not exists.");
                    return NotFound("User with this ID doesnt exist");
                }
                roleRepository.DeleteRole(id);
                logger.Log(LogLevel.Information, "DeleteRole", $"Role with id: {id} successfully deleted");
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "DeleteRole", $"Error whit delete role with ID: {id}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }


        }

    }
}
