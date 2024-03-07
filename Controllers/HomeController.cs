using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopAdir.Data;
using PetShopAdir.Models;
using System.Diagnostics;

namespace PetShopAdir.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DataContext _dataContext;


        public HomeController(ILogger<HomeController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
            _dataContext.Database.EnsureCreated();
        }

        public IActionResult Index()
        {
            var animals = _dataContext.Animals.Include(a => a.Comments).ToList();
            return View(animals);
        }

        public IActionResult Catalog (int selectedCategoryId = 0)
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

            if (selectedAnimal == null)
            {
                return NotFound(); 
            }

            return View(selectedAnimal);
        }
        [HttpPost]
        public IActionResult AddComment(int id, string commentText, string returnController)
        {
           
            var selectedAnimal = _dataContext.Animals
                .Include(a => a.Comments)
                .FirstOrDefault(a => a.Id == id);

            if (selectedAnimal == null)
            {
                return NotFound(); 
            }

       
            var newComment = new Comment
            {
                CommentText = commentText,
                AnimalId = selectedAnimal.Id
            };

            if (commentText != null)
            {
                _dataContext.Comments.Add(newComment);
                _dataContext.SaveChanges();
            }

           
           

          
            return RedirectToAction("Details", returnController, new { id });
        }


    }
}