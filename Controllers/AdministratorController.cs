using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShopAdir.Data;
using PetShopAdir.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace PetShopAdir.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly DataContext _dataContext;

        public AdministratorController(DataContext context)
        {
            _dataContext = context;
        }

        public IActionResult Index(int selectedCategoryId = 0)
        {
            var animalsQuery = _dataContext.Animals.AsQueryable();

            if (selectedCategoryId > 0)
            {
                animalsQuery = animalsQuery.Where(a => a.CategoryId == selectedCategoryId);
            }
            animalsQuery = animalsQuery.Include(a => a.Comments);

            var animals = animalsQuery.ToList();

            var categories = _dataContext.Categories.ToList();
            ViewBag.Categories = categories;


            ViewBag.SelectedCategoryId = selectedCategoryId;

            return View(animals);
        }

        public IActionResult Details(int id)
        {

            var selectedAnimal = _dataContext.Animals
                .Include(a => a.Comments)
                .Include(a => a.Category)
                .FirstOrDefault(a => a.Id == id);
            var categories = _dataContext.Categories.ToList();

            var categoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            ViewBag.Categories = new SelectList(categoryList, "Value", "Text");

            if (selectedAnimal == null)
            {
                return NotFound();
            }

            return View(selectedAnimal);
        }

        [HttpPost]
        public IActionResult DeleteComment(int id, int commentId)
        {
         
            var selectedAnimal = _dataContext.Animals
                .Include(a => a.Comments)
                .FirstOrDefault(a => a.Id == id);

            if (selectedAnimal == null)
            {
                return NotFound(); 
            }

           
            var commentToDelete = selectedAnimal.Comments.FirstOrDefault(c => c.Id == commentId);

            if (commentToDelete == null)
            {
                return NotFound(); 
            }

           
            selectedAnimal.Comments.Remove(commentToDelete);

          
            _dataContext.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }


        public IActionResult Delete(int id)
        {
            var selectedAnimal = _dataContext.Animals
                  .FirstOrDefault(a => a.Id == id);
            if (selectedAnimal != null)
            {
                _dataContext.Animals.Remove(selectedAnimal);
                _dataContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public IActionResult EditValue(int animalId, string fieldName, string newValue)
        {
            if (fieldName == null || newValue == null)
            {
                return RedirectToAction("Details", new { id = animalId });
            }
            var selectedAnimal = _dataContext.Animals
              .FirstOrDefault(a => a.Id == animalId);

            try
            {
                switch (fieldName)
                {
                    case "Category":
                        selectedAnimal.CategoryId = int.Parse(newValue);
                        break;
                    case "Name":
                        selectedAnimal.Name = newValue;
                        break;
                    case "Description":
                        selectedAnimal.Description = newValue;
                        break;
                    case "Age":
                        selectedAnimal.Age = int.Parse(newValue);
                        break;
                    
                }
            }
            finally
            {
                _dataContext.SaveChanges();
            }
            return RedirectToAction("Details", new { id = animalId });
        }
        public IActionResult EditPicture(int animalId, IFormFile newPicture)
        {
           
            
                if (newPicture != null && newPicture.Length > 0)
                {

                    string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");


                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }


                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + newPicture.FileName;


                    string filePath = Path.Combine(imagesFolder, uniqueFileName);


                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        newPicture.CopyTo(fileStream);
                    }
                    _dataContext.Animals.FirstOrDefault(a => a.Id == animalId).PictureName = $"/images/{uniqueFileName}";
                    _dataContext.SaveChanges();
                
               
            }
            return RedirectToAction("Details", new { id = animalId });
        }
        public IActionResult AddNew()
        {

            return View(_dataContext.Categories.ToList());
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Create(int categoryId, string name, int age, string description, IFormFile picture)
        {
            try
            {
                if (picture != null && picture.Length > 0)
                {
                   
                    string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                    
                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + picture.FileName;

                   
                    string filePath = Path.Combine(imagesFolder, uniqueFileName);

                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        picture.CopyTo(fileStream);
                    }

                    if (ModelState.IsValid)
                    {
                        var newAnimal = new Animal
                        {
                            CategoryId = categoryId,
                            Name = name,
                            Age = age,
                            Description = description,
                            PictureName = $"/images/{uniqueFileName}" 
                        };

                       
                        _dataContext.Animals.Add(newAnimal);
                        _dataContext.SaveChanges();

                       
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                   
                    ModelState.AddModelError("", "Please select a picture.");
                }
            }
            catch
            {
                
                ModelState.AddModelError("", "An error occurred while processing your request.");
            }

           
            return View("AddNew");
        }

    }
}
