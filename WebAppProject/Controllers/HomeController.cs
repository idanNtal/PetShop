using Microsoft.AspNetCore.Mvc;
using Service;
using Service.AnimelsServices;

namespace WebAppProject.Controllers
{
    public class HomeController : Controller
    {
        private IPetShopService _service;

        public HomeController(IPetShopService service) => _service = service;

        public IActionResult Welcome()
        {
            return View();
        }

        public async Task<IActionResult> HomePage()
        {
            var q = await _service.AnimalService.GetTopCommentsAminalsAsync(2);
            return View(q);
        }
    }
}
