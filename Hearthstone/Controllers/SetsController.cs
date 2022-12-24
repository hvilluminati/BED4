using Microsoft.AspNetCore.Mvc;
using Hearthstone.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Hearthstone.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hearthstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private readonly SetService _setService;
        private readonly ILogger<SetsController> _logger;
        public SetsController(SetService setService, ILogger<SetsController> logger)
        {
            _setService = setService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Set>> GetSets()
        {
            _logger.LogInformation("Get all sets");

            var set = await _setService.GetAsync();

            if (set == null)
                return NotFound();

            return Ok(set);
        }

        [HttpPost]
        public ActionResult SeedData()
        {
            _setService.CreateSets();
            return Ok();
        }

    }
}
