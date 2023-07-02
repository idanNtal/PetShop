using Microsoft.AspNetCore.Mvc;

namespace WebAppProject.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult PageNotFound(int statusCode)
        {
            Response.StatusCode = statusCode;
            ViewBag.statusCode = statusCode;
            return View();
        }

        public ActionResult IdNotFound(string id)
        {
            ViewBag.ID = id;
            return View();
        }
    }
}
