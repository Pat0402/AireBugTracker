using DatabaseContext.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

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
            await _bugService.CreateAsync(new Bug
            {
                Id = 1,
                Title = "First Bug",
                Details = "These are the details of the first bug",
                OpenedDate = DateTimeOffset.UtcNow,
                IsOpen = true
            });
            var bugs = await _bugService.GetAll();
            return Ok(bugs);
        }
    }
}