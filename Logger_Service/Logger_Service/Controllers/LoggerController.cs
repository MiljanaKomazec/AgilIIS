using Logger_Service.Data;
using Logger_Service.Model;
using Microsoft.AspNetCore.Mvc;

namespace Logger_Service.Controllers
{
    [Route("api/logger")]
    [ApiController]
    public class LoggerController : Controller
    {
        private readonly ILoggerRepository loggerRepository;

        public LoggerController(ILoggerRepository loggerRepository) 
        {
            this.loggerRepository = loggerRepository;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PostLogger([FromBody] LoggerModel logger)
        {
            try
            {
                string messages = "Service Name:" + logger.ServiceName + " *** " + "Method:" + logger.Method + " *** " + "Message:" + logger.Message;

                if (logger.LogLevel == LogLevel.Information) 
                { 
                    loggerRepository.InfoLogger(messages);
                }
                else if (logger.LogLevel == LogLevel.Warning)
                {
                    loggerRepository.WarnLogger(messages);
                }
                else if (logger.LogLevel == LogLevel.Debug)
                {
                    loggerRepository.DebugLogger(messages);
                }
                else if(logger.LogLevel == LogLevel.Error)
                {
                    loggerRepository.ErrorLogger(logger.Exc, messages);
                }

                
                var mess1 = Task.FromResult("A successful message!");
                return Ok(mess1);
            }
            catch (Exception ex)
            {
                loggerRepository.ErrorLogger(ex, "There's been an error");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when logging into the log file.");
            }
        }
    }
}
