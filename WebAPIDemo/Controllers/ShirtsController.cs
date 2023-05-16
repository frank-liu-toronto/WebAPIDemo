using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;
using WebAPIDemo.Models.Repositories;

namespace WebAPIDemo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController: ControllerBase
    {
        [HttpGet]        
        public IActionResult GetShirts()
        {
            return Ok("Reading all the shirts");
        }

        [HttpGet("{id}")]        
        public IActionResult GetShirtById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var shirt = ShirtRepository.GetShirtById(id);
            if (shirt == null)
                return NotFound();

            return Ok(shirt);
        }

        [HttpPost]        
        public IActionResult CreateShirt([FromBody]Shirt shirt)
        {
            return Ok("Creating a shirt");
        }

        [HttpPut("{id}")]        
        public IActionResult UpdateShirt(int id)
        {
            return Ok($"Updating shirt: {id}");
        }

        [HttpDelete("{id}")]        
        public IActionResult DeleteShirt(int id)
        {
            return Ok($"Deleting shirt: {id}");
        }
    }
}
