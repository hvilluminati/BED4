using Microsoft.AspNetCore.Mvc;
using Hearthstone.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Hearthstone.Models;

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

        [HttpGet]
        public async Task<ActionResult<CardType>> GetClasses()
        {
            _logger.LogInformation("Get all classes");

            var classType = await _classService.GetAsync();

            if (classType == null)
                return NotFound();

            return Ok(classType);
        }

        [HttpPost]
        public ActionResult SeedData()
        {
            _classService.CreateClasses();
            return Ok();
        }

    }
}
