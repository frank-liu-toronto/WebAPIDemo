using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Repositories;

namespace WebApp.Controllers
{
    public class ShirtsController : Controller
    {
        private readonly IWebApiExecuter webApiExecuter;

        public ShirtsController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IActionResult> Index()
        {
            return View(await webApiExecuter.InvokeGet<List<Shirt>>("shirts"));
        }

        public IActionResult CreateShirt() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateShirt(Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecuter.InvokePost("shirts", shirt);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch(WebApiException ex)
                {
                    HandleWebApiException(ex);
                }
                
            }

            return View(shirt);
        }

        public async Task<IActionResult> UpdateShirt(int shirtId)
        {
            try
            {
                var shirt = await webApiExecuter.InvokeGet<Shirt>($"shirts/{shirtId}");
                if (shirt != null)
                {
                    return View(shirt);
                }
            }
            catch(WebApiException ex)
            {
                HandleWebApiException(ex);
                return View();
            }
            

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShirt(Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecuter.InvokePut($"shirts/{shirt.ShirtId}", shirt);
                    return RedirectToAction(nameof(Index));
                }
                catch(WebApiException ex)
                {
                    HandleWebApiException(ex);
                }
                
            }

            return View(shirt);
        }

        public async Task<IActionResult> DeleteShirt(int shirtId)
        {
            try
            {
                await webApiExecuter.InvokeDelete($"shirts/{shirtId}");
                return RedirectToAction(nameof(Index));
            }
            catch(WebApiException ex)
            {
                HandleWebApiException(ex);
                return View(nameof(Index),
                    await webApiExecuter.InvokeGet<List<Shirt>>("shirts"));
            }
        }

        private void HandleWebApiException(WebApiException ex)
        {
            if (ex.ErrorResponse != null &&
                ex.ErrorResponse.Errors != null &&
                ex.ErrorResponse.Errors.Count > 0)
            {
                foreach (var error in ex.ErrorResponse.Errors)
                {
                    ModelState.AddModelError(error.Key, string.Join("; ", error.Value));
                }
            }
            else if (ex.ErrorResponse != null)
            {
                ModelState.AddModelError("Error", ex.ErrorResponse.Title);
            }
            else
            {
                ModelState.AddModelError("Error", ex.Message);
            }
        }
    }
}
