using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Repositories;

namespace WebApp.Controllers
{
    public class ShirtsController : Controller
    {
        public IActionResult Index()
        {
            return View(ShirtRepository.GetShirts());
        }
    }
}
