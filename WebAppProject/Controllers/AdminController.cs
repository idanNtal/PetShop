using System;
using System.Drawing.Printing;
using System.IO;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Packaging.Signing;
using Service;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Admin, Management")]
    public class AdminController : Controller
    {
        private readonly IPetShopService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IPetShopService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> AdminPage()
        {
            var categories = await _service.CategoryService.GetAllCategoriesWithAminalsAsync();
            return View(categories);
        }

        public async Task<IActionResult> DeleteAnimal(int animalId)
        {
            if (ModelState.IsValid)
            {
                if (_service.AnimalService.IsIdExist(animalId))
                {
                    await _service.AnimalService.DeleteAnimalAsync(animalId);
                }
            }
            return RedirectToAction("AdminPage");
        }

        [HttpGet]
        public async Task<IActionResult> AddEditAnimalPage(int animalId = -1)
        {
            var animal = await _service.AnimalService.GetAnimalByIdAsync(animalId);
            if (animal == null && animalId != -1)
                return RedirectToAction("IdNotFound", "Error", new { id = animalId });

            ViewBag.Categories = await _service.CategoryService.GetAllCategoriesAsync();
            ViewBag.Mode = animal == null ? "Add" : "Edit";
            return View(animal);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditAnimal(Animal newAnimal)
        {
            ModelState.Remove("PictureName");
            ModelState.Remove("Picture");
            var picture = newAnimal.Picture;
            if (ModelState.IsValid && _service.AnimalService.IsNameAvailable(newAnimal.Name!, newAnimal.Id))
            {
                newAnimal.PictureName = newAnimal.Name + ".png";
                string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Animals");

                if (newAnimal.Id == 0) // Add Mode 
                {
                    UploadImage(newAnimal.PictureName, picture!);

                    await _service.AnimalService.InsertAnimalAsync(newAnimal);
                }
                else if (newAnimal.Id != 0)  // Edit Mode
                {
                    var animal = await _service.AnimalService.GetAnimalByIdAsync(newAnimal.Id);
                    if (picture != null)
                    {
                        DeleteImage(animal.PictureName!);
                        UploadImage(newAnimal.PictureName, picture);
                    }
                    else if (picture == null && animal.Name != newAnimal.Name)
                    {
                        ChangeImagePath(animal.PictureName!, newAnimal.PictureName);
                    }

                    await _service.AnimalService.EditAnimalAsync(newAnimal);
                }
            }
            return RedirectToAction("AdminPage");
        }
        public async Task<IActionResult> removeComments(IEnumerable<int> commentsIDs, string userId) 
        {
            var comments = await _service.CommentService.GetCommentsByIdsAsync(commentsIDs);
            await _service.CommentService.RemoveCommentsAsync(comments);
            return RedirectToAction("UserInfo", "Account", new { userId = userId });
        }


        private void ChangeImagePath(string OldImageName, string NewImageName)
        {
            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Animals", OldImageName); // Replace with the actual path of your image file
            string newImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Animals", NewImageName); // Replace with the desired new path and name

            if (System.IO.File.Exists(oldImagePath))
            {
                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(newImagePath);
                // Rename the file by moving it to the new path
                System.IO.File.Move(oldImagePath, newImagePath);
            }
        }

        private void UploadImage(string PictureName, IFormFile picture)
        {
            //var imagePath = "wwwroot/Images/Animals/" + name + ".png";
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Animals", PictureName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                picture?.CopyTo(fileStream);
            }
        }

        private void DeleteImage(string OldPictureName)
        {
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Animals", OldPictureName);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }


        // ### Remote Attribute (For Aminal Model) ###
        public async Task<IActionResult> IsPngImage(string picture)
        {
            var extension = Path.GetExtension(picture);
            return extension.ToLower() == ".png" ? Json(true) : Json($"The extension - {extension} is invalid, only '.Png' allow.");
        }
        public async Task<IActionResult> IsNameUsed(string Name, int Id = -1)
        {
            var res = _service.AnimalService.IsNameAvailable(Name, Id);
            return res ? Json(true) : Json($"The name - {Name} is already taken.");
        }

    }
}
