using Microsoft.AspNetCore.Mvc;
using Hearthstone.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Hearthstone.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hearthstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RaritiesController : ControllerBase
    {
        private readonly RarityService _rarityService;
        private readonly ILogger<RaritiesController> _logger;
        public RaritiesController(RarityService rarityService, ILogger<RaritiesController> logger)
        {
            _rarityService = rarityService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CardType>> GetRarities()
        {
            _logger.LogInformation("Get all rarities");

            var rarity = await _rarityService.GetAsync();

            if (rarity == null)
                return NotFound();

            return Ok(rarity);
        }

        [HttpPost]
        public ActionResult SeedData()
        {
            _rarityService.CreateRarities();
            return Ok();
        }

    }
}
