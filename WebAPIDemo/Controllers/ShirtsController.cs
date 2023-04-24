using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController: ControllerBase
    {
        [HttpGet]        
        public string GetShirts()
        {
            return "Reading all the shirts";
        }

        [HttpGet("{id}")]        
        public string GetShirtById(int id)
        {
            return $"Reading shirt: {id}";
        }

        [HttpPost]        
        public string CreateShirt()
        {
            return $"Creating a shirt";
        }

        [HttpPut("{id}")]        
        public string UpdateShirt(int id)
        {
            return $"Updating shirt: {id}";
        }

        [HttpDelete("{id}")]        
        public string DeleteShirt(int id)
        {
            return $"Deleting shirt: {id}";
        }
    }
}
