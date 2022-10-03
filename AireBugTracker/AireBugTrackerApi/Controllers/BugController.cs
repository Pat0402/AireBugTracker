using AireBugTrackerApi.Helpers;
using DatabaseContext.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Diagnostics;

namespace AireBugTrackerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugController : ControllerBase
    {
        private readonly ILogger<BugController> _logger;
        private IBugService _bugService;

        public BugController(ILogger<BugController> logger, IBugService bugService)
        {
            _logger = logger;
            _bugService = bugService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var theBugs = await _bugService.GetAllAsync();
            _logger.LogDebug(LogEvents.GetItems, theBugs.Message);

            return Ok(theBugs.Target);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var theBug = await _bugService.GetByIdAsync(id);
                _logger.LogDebug(LogEvents.GetItem, theBug.Message);

                return Ok(theBug);
            }
            catch(Exception e)
            {
                _logger.LogError(LogEvents.GetItemInternalError, e, null);

                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Bug value)
        {
            try
            {
                value.OpenedDate = DateTimeOffset.UtcNow;
                value.IsOpen = true;
                var theResult = await _bugService.CreateAsync(value);

                return StatusCode(((int)theResult.Status));
            }
            catch (Exception e)
            {
                _logger.LogError(LogEvents.CreateItemInternalError, e, null);

                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Bug value)
        {
            try
            {
                var theResult = await _bugService.UpdateAsync(value);

                return StatusCode(((int)theResult.Status));
            }
            catch (Exception e)
            {
                _logger.LogError(LogEvents.UpdateItemInternalError, e, null);

                return StatusCode(500);
            }
        }
    }
}