using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UserStory.Data.DataFunctionallity;
using UserStory.Data.DataTask;
using UserStory.Data.DataUserStory;
using UserStory.Helpers;
using UserStory.Models.ModelFunctionality;
using UserStory.Models.ModelTask;
using UserStory.Models.ModelUserStory;

namespace UserStory.Controllers
{
    [ApiController]
    [Route("api/userStory/functionality/task")]
    [Produces("application/json", "application/xml")]
    public class TaskController : Controller
    {
        private readonly ITaskRepository taskRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public TaskController (ITaskRepository taskRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.taskRepository = taskRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<TaskDTO>>> GetTask()
        {
            var tasks = await taskRepository.GetTask();
            if (tasks == null || tasks.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetAllTask", "List of tasks is empty.");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetAllTask", "Tasks successfully restored");
            return Ok(tasks);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("{taskId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<TaskDTO>> GetTaskById(Guid taskId)
        {
            var task = await taskRepository.GetTaskById(taskId);

            if (task == null)
            {
                await loggerService.Log(LogLevel.Warning, "GetTaskById", $"Task with ID: {taskId} not found.");
                return NotFound();
            }

            await loggerService.Log(LogLevel.Information, "GetTaskById", $"Task with ID: {taskId} successfully restored");
            return Ok(mapper.Map<TaskDTO>(task));
        }

        [HttpPost]
        [Consumes("application/json")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskConfirmationDTO>> CreateTask([FromBody] TaskCreationDTO task)
        {
            try
            {
                TaskE task1 = mapper.Map<TaskE>(task);
                task1.TaskId = Guid.NewGuid();
                Console.WriteLine(task1);

                var taskValid = await ValidateTask(task1);

                if (!taskValid)
                {
                    await loggerService.Log(LogLevel.Warning, "CreateTask", $"Task with this ID already exist. Please enter valid ID.");
                    return BadRequest("Task with this ID already exist. Please enter valid ID.");
                }

                TaskConfirmation confirmation = await taskRepository.CreateTask(task1);
                await taskRepository.SaveChanges();
                //string location = linkGenerator.GetPathByAction("GetTask", "TaskE", new { taskId = confirmation.TaskId });
                await loggerService.Log(LogLevel.Information, "CreateTask", $"Task with values: {JsonConvert.SerializeObject(task)} successfully created");
                return Ok(mapper.Map<TaskConfirmationDTO>(confirmation));
            }
            catch(Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "CreateTask", $"An error occurred while entering the task with values: {JsonConvert.SerializeObject(task)}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        private async Task<bool> ValidateTask(TaskE task)
        {
            List<TaskE> tasks = await taskRepository.GetTask();
            foreach (TaskE task1 in tasks)
            {
                if (task1.TaskId == task.TaskId)
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
        public async Task<ActionResult<TaskDTO>> UpdateTask(TaskUpdateDTO task)
        {
            try
            {
                //Provera da li postoji task koji zelimo da promenimo
                var oldTask = await taskRepository.GetTaskById(task.TaskId);
                if (oldTask == null)
                {

                    await loggerService.Log(LogLevel.Warning, "UpdateTask", $"Task with ID: {task.TaskId} does not exist");
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                TaskE taskEntity = mapper.Map<TaskE>(task);

                mapper.Map(taskEntity, oldTask);

                await taskRepository.SaveChanges();
                await loggerService.Log(LogLevel.Information, "UpdateTask", $"Task with ID: {task.TaskId} successfully updated. The old values -> {oldTask} are replaced.");
                return Ok(mapper.Map<UserStoryDTO>(oldTask));
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "UpdateTask", $"Error when editing task with ID: {task.TaskId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error:" + ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteTask(Guid taskId)
        {
            try
            {
                var task = await taskRepository.GetTaskById(taskId);

                if (task == null)
                {
                    await loggerService.Log(LogLevel.Warning, "DeleteTask", $"Task with ID: {taskId} does not exist");
                    return NotFound();
                }

                await taskRepository.DeleteTask(taskId);
                await taskRepository.SaveChanges();
                await loggerService.Log(LogLevel.Information, "DeleteTask", $"Task with ID: {taskId} successfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "DeleteTask", $"An error occured while deleting task with ID: {taskId}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("functionality/{functionalityId}")]
        public async Task<ActionResult<List<TaskDTO>>> GetTaskByFunctionality(Guid functionalityId)
        {
            var tasks = await taskRepository.GetTaskByFunctionality(functionalityId);

            if (tasks == null || tasks.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetTaskByFunctionality", $"Task where FunctionalityID: {functionalityId} does not exist");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetTaskByFunctionality", $"Tasks where FunctionalityID: {functionalityId} successfully restored");
            return Ok(mapper.Map<List<TaskDTO>>(tasks));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowOrigin")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("sprint/{sprintId}")]
        public async Task<ActionResult<List<TaskDTO>>> GetTasksBySprintId(Guid sprintId)
        {
            var tasks = await taskRepository.GetTasksBySprintId(sprintId);

            if (tasks == null || tasks.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetTasksBySprintId", "Tasks with this SprintID does not exist");
                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "GetTasksBySprintId", "Tasks with this SprintID successfully restored");
            return Ok(tasks);
        }

    }
}
