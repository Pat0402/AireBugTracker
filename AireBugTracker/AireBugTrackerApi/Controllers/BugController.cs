using AireBugTrackerApi.Helpers;
using DatabaseContext.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using System.Diagnostics;

namespace AireBugTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Bug[]))]
        public async Task<IActionResult> Get()
        {
            var theBugs = await _bugService.GetAllAsync();
            _logger.LogDebug(LogEvents.GetItems, theBugs.Message);

            return Ok(theBugs.Target);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Bug))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var theBug = await _bugService.GetByIdAsync(id);
                _logger.LogDebug(LogEvents.GetItem, theBug.Message);

                return Ok(theBug.Target);
            }
            catch(Exception e)
            {
                _logger.LogError(LogEvents.GetItemInternalError, e, null);

                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Bug))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] BugDTO bug)
        {
            try
            {
                var response = await _bugService.CreateAsync(bug);
                _logger.LogDebug(LogEvents.CreateItem, response.Message);

                return StatusCode((int)response.Status, response.Target);
            }
            catch (Exception e)
            {
                _logger.LogError(LogEvents.CreateItemInternalError, e, null);

                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] BugDTO bug)
        {
            try
            {
                var response = await _bugService.UpdateAsync(id, bug);
                _logger.LogDebug(LogEvents.UpdateItem, response.Message);

                return StatusCode((int)response.Status, response.Target);
            }
            catch (Exception e)
            {
                _logger.LogError(LogEvents.UpdateItemInternalError, e, null);

                return StatusCode(500);
            }
        }
    }
}