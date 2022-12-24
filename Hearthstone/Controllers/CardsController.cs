using Microsoft.AspNetCore.Mvc;
using Hearthstone.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Hearthstone.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hearthstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly CardService _cardService;
        private readonly ILogger<CardsController> _logger;
        public CardsController(CardService cardService, ILogger<CardsController> logger)
        {
            _cardService = cardService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardWithMetaDataDTO>>> GetCardsAsync([FromQuery] QueryParameter queryParameter)
        {
            _logger.LogInformation(
                $"page={queryParameter.page} " +
                $"artist={queryParameter.artist} " +
                $"rarirtyId={queryParameter.rarityid} " +
                $"classId={queryParameter.classid} " +
                $"setId={queryParameter.setid} ");

            var result = await _cardService.GetCardsByQuery(queryParameter);

            _logger.LogInformation($"EntityCount={result.Count} ");

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult SeedData()
        {
            _cardService.CreateCards();
            return Ok();
        }

    }
}
