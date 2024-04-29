using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoryPointAPI.DTO;
using StoryPointAPI.Models;
using StoryPointAPI.Services;
using StoryPointAPI.Helpers;

namespace StoryPointAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryPointController : ControllerBase
    {
        private readonly StoryPointContext _context;
        private readonly ILoggerService loggerService;
        private readonly StoryPointService storyPointService;


        public StoryPointController(StoryPointContext context, ILoggerService loggerService, StoryPointService storyPointService)
        {
            _context = context;
            this.loggerService = loggerService;
            this.storyPointService = storyPointService;

        }

        #region HTTP_METHODS
        // GET: api/StoryPoint
        //GET ALL
        //DA NE BUDE DTO DA BI MOGAO ADMIN DA POGLEDA SVE ID-eve I IZABERE AKO MU JE POTREBNO DA EDITUJE NEKI POSEBNO
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<StoryPoint>>> GetStoryPoints()
        {
          if (_context.StoryPoints == null)
          {
               await loggerService.Log(LogLevel.Warning, "GetAllStoryPoints", "List of story point values is empty.");
               return NotFound();
          }
            await loggerService.Log(LogLevel.Information, "GetAllStoryPoints", "Story points successfully retrieved");
            return await _context.StoryPoints
                .ToListAsync();
        }

        // GET: api/StoryPoint/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StoryPointDTO>> GetStoryPoint(Guid id)
        {
          if (_context.StoryPoints == null)
          {
                await loggerService.Log(LogLevel.Warning, "GetStoryPointById", $"Story point with ID: {id} not found.");
                return NotFound();
          }
            var storyPoint = await _context.StoryPoints.FindAsync(id);

            if (storyPoint == null)
            {
                await loggerService.Log(LogLevel.Warning, "GetStoryPointById", $"Story point with ID: {id} found but not retrieved.");
                return NotFound();
            }

            // Story point u DTO
            var storyPointDTO = StoryPointToDTO(storyPoint);

            await loggerService.Log(LogLevel.Information, "GetStoryPointById", $"Story point with ID: {id} successfully retrieved.");
            // Vracaj DTO u OK response
            return Ok(storyPointDTO);
        }

        //NIJE DTO?
        // PUT: api/StoryPoint/5 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutStoryPoint(Guid id, StoryPoint storyPoint)
        {
            if (id != storyPoint.StoryPointId)
            {
                await loggerService.Log(LogLevel.Warning, "UpdateStoryPoint", $"Story point with ID: {id} does not match sent story point");
                return BadRequest();
            }

            _context.Entry(storyPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!StoryPointExists(id))
                {
                    await loggerService.Log(LogLevel.Warning, "UpdateStoryPoint", $"Story point with ID: {id} does not exist");
                    return NotFound();
                }
                else
                {
                    //throw
                    await loggerService.Log(LogLevel.Error, "UpdateStoryPoint", $"Error when editing story point with ID: {id}.", ex);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Update error:" + ex.Message);
                    
                }
            }
            await loggerService.Log(LogLevel.Information, "UpdateStoryPoint", $"Story point with ID: {id} successfully updated. The old value is replaced by {storyPoint.ValueStoryPoint}.");
            return NoContent();
        }

        // POST: api/StoryPoint
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoryPointDTO>> PostStoryPoint(StoryPointDTO storyPointDTO)
        {
          if (_context.StoryPoints == null)
          {
                await loggerService.Log(LogLevel.Warning, "CreateStoryPoint", $"StoryPoint ID is null");
                return Problem("Entity set 'StoryPointContext.StoryPoints'  is null.");
          }
            var storyPoint = new StoryPoint
            {
                ValueStoryPoint = storyPointDTO.ValueStoryPoint,
                UserStoryRootId = storyPointDTO.UserStoryRootId
            };

            _context.StoryPoints.Add(storyPoint);
            await _context.SaveChangesAsync();
            await loggerService.Log(LogLevel.Information, "CreateStoryPoint", $"StoryPoint with values: {JsonConvert.SerializeObject(storyPointDTO.ValueStoryPoint)} successfully created");
            //return CreatedAtAction("GetStoryPoint", new { id = storyPoint.StoryPointId }, storyPoint);
            return CreatedAtAction(nameof(GetStoryPoint), new { id = storyPoint.StoryPointId }, StoryPointToDTO(storyPoint));
        }

        // DELETE: api/StoryPoint/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoryPoint(Guid id)
        {
            if (_context.StoryPoints == null)
            {
                await loggerService.Log(LogLevel.Warning, "DeleteStoryPoint", $"Story point with ID: {id} does not exist");
                return NotFound();
            }
            var storyPoint = await _context.StoryPoints.FindAsync(id);
            if (storyPoint == null)
            {
                await loggerService.Log(LogLevel.Warning, "DeleteStoryPoint", $"Story point with ID: {id} error while fetching.");
                return NotFound();
            }

            _context.StoryPoints.Remove(storyPoint);
            await _context.SaveChangesAsync();
            await loggerService.Log(LogLevel.Information, "DeleteUserStory", $"Story point with ID: {id} successfully deleted.");
            return NoContent();
        }

        private bool StoryPointExists(Guid id)
        {
            return (_context.StoryPoints?.Any(e => e.StoryPointId == id)).GetValueOrDefault();
        }
        #endregion HTTP_METHODS

        //DTO
        private static StoryPointDTO StoryPointToDTO(StoryPoint storyPoint) =>
        new StoryPointDTO
        {
           ValueStoryPoint = storyPoint.ValueStoryPoint,
           UserStoryRootId = storyPoint.UserStoryRootId
           //Dodati druga obelezja..?
        };

        //FromDTO -> Lazar je morao ovo da implementira zato sto je morao da translira nazad u "pravi" objekat.

        [HttpPut("update-final-loc/{storyPointId}")]
        public async Task<IActionResult> UpdateFinalLOC(Guid storyPointId, [FromBody] StoryPointUpdateRequest request)
        {
            // Vratiti story point iz baze (id)
            var storyPoint = await _context.StoryPoints.FindAsync(storyPointId);

            if (storyPoint == null)
            {
                await loggerService.Log(LogLevel.Warning, "UpdateLOC", $"Story point with ID: {storyPointId} not found");
                return NotFound(); 
            }

            
            var planningPokerManager = new PlanningPokerManager();

            //Racunaj LOC
            var levelOfComplexity = planningPokerManager.CalculateFinalLOC(request.FirstHandVotes, request.SecondHandVotes);

            // ako je LOC (glasanje) 0 vratiti poruku
            if (levelOfComplexity.FinalLOC == 0)
            {
                await loggerService.Log(LogLevel.Information, "UpdateLOC", $"Round of voting repeated.");
                return UnprocessableEntity("No clear winner. Please vote again.");
            }

            // Story point koji je vracen iz baze se update-uje i daje mu se ValueStoryPoint (FinalLOC)
            storyPoint.ValueStoryPoint = levelOfComplexity.FinalLOC;
            //Console.WriteLine($"Level of Complexity - Final LOC: {levelOfComplexity.FinalLOC}");
            await loggerService.Log(LogLevel.Information, "UpdateLOC", $"Level of Complexity - Final LOC: {levelOfComplexity.FinalLOC}");
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoryPointExists(storyPointId))
                {
                    await loggerService.Log(LogLevel.Warning, "UpdateLOC", $"Story point with ID: {storyPointId} not found while updating LOC.");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            await loggerService.Log(LogLevel.Information, "UpdateLOC", $"Story point with ID: {storyPointId} successfully updated with final LOC: {levelOfComplexity.FinalLOC}.");
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("userStory/{userStoryId}")]
        public ActionResult<List<StoryPointDTO>> GetStoryPointsByUserStoryId(Guid userStoryId)
        {
            var points = storyPointService.GetStoryPointsByUserStoryId(userStoryId);


            if (points == null)
            {
                loggerService.Log(LogLevel.Warning, "GetStoryPointsByUserStoryId", "Points with user story id not found");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetStoryPointsByUserStoryId", "Points successfuly restored.");
            return Ok(points);
        }


    }
}
