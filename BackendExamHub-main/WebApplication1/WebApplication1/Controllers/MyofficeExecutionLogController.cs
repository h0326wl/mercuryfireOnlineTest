using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyofficeExecutionLogController : ControllerBase
    {
        private readonly MyofficeExecutionLogService _logService;

        public MyofficeExecutionLogController(MyofficeExecutionLogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MyofficeExcuteionLog>> GetAllLogs()
        {
            return Ok(_logService.GetAllLogs());
        }

        [HttpPost]
        public ActionResult AddLog([FromBody] MyofficeExcuteionLog log)
        {
            _logService.AddLog(log);
            return CreatedAtAction(nameof(GetAllLogs), new { id = log.DeLogAuthId }, log);
        }
    }
}
