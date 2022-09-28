using Microsoft.AspNetCore.Mvc;

namespace AireBugTrackerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugController : ControllerBase
    {
        private readonly ILogger<BugController> _logger;

        public BugController(ILogger<BugController> logger)
        {
            _logger = logger;
        }
    }
}