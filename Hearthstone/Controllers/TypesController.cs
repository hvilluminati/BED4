using Microsoft.AspNetCore.Mvc;
using Hearthstone.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Hearthstone.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hearthstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly CardTypeService _cardTypeService;
        private readonly ILogger<TypesController> _logger;
        public TypesController(CardTypeService cardTypeService, ILogger<TypesController> logger)
        {
            _cardTypeService = cardTypeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CardType>> GetCardTypes()
        {
            _logger.LogInformation("Get all card types");

            var cardType = await _cardTypeService.GetAsync();

            if (cardType == null)
                return NotFound();

            return Ok(cardType);
        }

        [HttpPost]
        public ActionResult SeedData()
        {
            _cardTypeService.CreateCardTypes();
            return Ok();
        }

    }
}
