using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChefsDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsDishes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;        

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.allDishes = _context.Dishes.Include(dish => dish.Creator).ToList();
            ViewBag.allChefs = _context.Chefs.OrderBy(d => d.FirstName).ToList();
            return View();
        }

        [HttpGet("NewChef")]
        public IActionResult NewChef()
        {
            return View();
        }

        [HttpGet("Dishes")]
        public IActionResult Dishes()
        {
            // ViewBag.allDishes = _context.Dishes.OrderBy(d => d.Name).ToList();
            ViewBag.allDishes = _context.Dishes.Include(dish => dish.Creator).ToList();
            return View();
        }

        [HttpPost("CreateChef")]
        public IActionResult CreateChef(Chef nChef, DateTime DoB, int age)
        {
            if(DateTime.Now.Year - DoB.Year < 18)
                {
                    ModelState.AddModelError("Age", "Invalid Date, Must be at least 18!");
                    return View("NewChef");
                }

            if(ModelState.IsValid)
            {
                if(_context.Chefs.Any(u => u.FirstName == nChef.FirstName && u.LastName == nChef.LastName))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("FirstName", "Chef Already Exists!");
                    
                    return View("NewChef");
                } 
                age = DateTime.Now.Year - DoB.Year;
                nChef.Age = age;
                _context.Add(nChef);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("NewChef");
        }

        [HttpGet("NewDish")]
        public IActionResult NewDish()
        {
            ViewBag.allChefs = _context.Chefs.ToList();
            return View();
        }

        [HttpPost("addDish")]
        public IActionResult AddDish(Dish ndish)
        {
            if(ModelState.IsValid)
            {
                _context.Add(ndish);
                _context.SaveChanges();
                return RedirectToAction("Dishes");
            }
            else
            {
                ViewBag.allChefs = _context.Chefs.ToList();
                return View("NewDish");
            }
        }










        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
