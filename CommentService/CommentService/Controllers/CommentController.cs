using AutoMapper;
using CommentService.Data;
using CommentService.Helper;
using CommentService.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.Controllers
{
    [ApiController]
    [Route("api/comment")]
    [Produces("application/json", "application/xml")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public CommentController(ICommentRepository commentRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.commentRepository = commentRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }


        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<CommentDTO>> GetComment()
        {
            List<Comment> comment = commentRepository.GetComment();
            if (comment == null || comment.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetComment", "List of comments is empty.");
                return NoContent();
            }

            loggerService.Log(LogLevel.Information, "GetComment", "Comments successfuly restored.");
            return Ok(mapper.Map<List<CommentDTO>>(comment));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{commentId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<CommentDTO> GetCommentById(Guid commentId)
        {
            var comment = commentRepository.GetCommentById(commentId);

            if (comment == null)
            {
                loggerService.Log(LogLevel.Warning, "GetBacklogItemById", "Comment with id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetBacklogItemById", "Comment successfuly restored.");
            return Ok(mapper.Map<CommentDTO>(comment));
        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public ActionResult<CommentConfirmationDTO> CreateComment([FromBody] CommentCreateDTO comment)
        {


            try
            {

                Comment commentModel = mapper.Map<Comment>(comment);
                CommentConfirmation confirmation = commentRepository.CreateComment(commentModel);
                commentRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetComment", "Comment", new { commentId = confirmation.CommentId });
                loggerService.Log(LogLevel.Information, "CreateComment", "Comment successfuly created.");
                return Ok(mapper.Map<CommentConfirmationDTO>(confirmation));


            }
            catch
            {
                loggerService.Log(LogLevel.Error, "CreateComment", "An error occured while entering the comment.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }

        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableCors("AllowOrigin")]
        public ActionResult<CommentDTO> UpdateComment(CommentUpdateDTO comment)
        {
            try
            {

                var oldComment = commentRepository.GetCommentById(comment.CommentId);
                if (oldComment == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateComment", "Comment with id not found");
                    return NotFound();
                }
                Comment commentEntity = mapper.Map<Comment>(comment);

                mapper.Map(commentEntity, oldComment);

                commentRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "UpdateComment", "Comment successfuly updated.");
                return Ok(mapper.Map<CommentDTO>(oldComment));
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Error, "UpdateComment", "Error occured while editing the comment.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{commentId}")]
        [EnableCors("AllowOrigin")]
        public IActionResult DeleteComment(Guid commentId)
        {
            try
            {
                var comment = commentRepository.GetCommentById(commentId);

                if (comment == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteComment", "Comment with id not found");
                    return NotFound();
                }

                commentRepository.DeleteComment(commentId);
                commentRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteComment", "Comment successfuly deleted.");
                return NoContent();
            }
            catch (Exception)
            {
                loggerService.Log(LogLevel.Error, "DeleteComment", "An error occured while deleting the comment.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("userStory/{userStoryId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult <List<CommentDTO>> GetCommentByUserStoryId(Guid userStoryId)
        {
            var comment = commentRepository.GetCommentByUserStoryId(userStoryId);


            if (comment == null)
            {
                loggerService.Log(LogLevel.Warning, "GetBacklogItemByUserStoryId", "Comment with user story id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetBacklogItemByUserStoryId", "Comment successfuly restored.");
            return Ok(mapper.Map<List<CommentDTO>>(comment));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("user/{userId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<List<CommentDTO>> GetCommentByUserId(Guid userId)
        {
            var comment = commentRepository.GetCommentByUserId(userId);


            if (comment == null)
            {
                loggerService.Log(LogLevel.Warning, "GetCommentByUserId", "Comment with user id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetCommentByUserId", "Comment successfuly restored.");
            return Ok(mapper.Map<List<CommentDTO>>(comment));
        }
    }
}
