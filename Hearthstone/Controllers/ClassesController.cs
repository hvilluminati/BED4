using Microsoft.AspNetCore.Mvc;
using Hearthstone.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hearthstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ClassService _classService;
        private readonly ILogger<ClassesController> _logger;
        public ClassesController(ClassService classService, ILogger<ClassesController> logger)
        {
            _classService = classService;
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
            _classService.CreateClasses();
            return Ok();
        }

    }
}
