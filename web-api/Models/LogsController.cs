using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using web_api.Filters;

namespace web_api.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogger<LogsController> logger;
        private readonly LogsProcessor logsProcessor;

        public LogsController(ILogger<LogsController> log, LogsProcessor logProc) {
            logger = log;
            logsProcessor = logProc;
        }

        [HttpGet]
        [Route("")]
        public Task<Log[]> GetAll()
        {
            logger.LogInformation("Getting all the audit logs");
            return logsProcessor.GetAllLogs();
        }
    }
}