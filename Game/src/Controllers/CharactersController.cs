using Microsoft.AspNetCore.Mvc;

namespace Game.Controllers
{
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            return Ok("dupa");
        }
    }
}
