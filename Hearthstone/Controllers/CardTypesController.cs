using Microsoft.AspNetCore.Mvc;
using Hearthstone.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hearthstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardTypesController : ControllerBase
    {
        private readonly CardTypeService _cardTypeService;
        private readonly ILogger<CardTypesController> _logger;
        public CardTypesController(CardTypeService cardTypeService, ILogger<CardTypesController> logger)
        {
            _cardTypeService = cardTypeService;
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
            _cardTypeService.CreateCardTypes();
            return Ok();
        }

    }
}
