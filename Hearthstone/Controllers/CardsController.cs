using Microsoft.AspNetCore.Mvc;
using Hearthstone.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

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

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult SeedData()
        {
            _cardService.CreateCards();
            return Ok();
        }

    }
}
