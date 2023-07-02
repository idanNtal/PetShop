using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service;
using Service.AnimelsServices;
using System.Security.Claims;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class CatalogController : Controller
    {
        readonly IPetShopService _service;

        public CatalogController(IPetShopService service) => _service = service;

        public async Task<IActionResult> CatalogPage()
        {
            var categories = await _service.CategoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> AnimalDetails(int id)
        {
            var animal = await _service.AnimalService.GetAnimalByIdAsync(id);
            if (animal == null)
                return RedirectToAction("IdNotFound", "Error", new { id = id });

            ViewBag.Aminal = animal;
            ViewBag.AminalID = animal.Id;
            ViewBag.CommentCount = animal.Comments!.Count();
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserID = userid;
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment newComment,bool showName)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(newComment.comment)
                    && _service.AnimalService.IsIdExist(newComment.AnimalId))
                {
                    if (showName)
                        newComment.comment = User.Identity!.Name + ": " + newComment.comment;

                    await _service.CommentService.AddCommentAsync(newComment);
                }
            }
            return RedirectToAction("AnimalDetails", new { id = newComment.AnimalId });
        }
    }
}
