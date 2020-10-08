using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Delicious.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Delicious.Controllers     
{
    public class HomeController : Controller   
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }
        
        [HttpGet("")]      
        
        public ViewResult Index()
        {
            List<Dish> AllDishes = _context.Dishes.ToList();
            return View("Index", AllDishes);
        }

        [HttpGet("new")]
        public IActionResult CreateDish()
        {
            return View("CreateDish");
        }

        [HttpPost("new")]
        public IActionResult CreateDish(Dish FromForm)
        {
            _context.Add(FromForm);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{DishId}")]
        public IActionResult UpdateDish(int DishId)
        {
            Dish Retrieved = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
            if(Retrieved == null)
            {
                return RedirectToAction("Index");
            }
            return View("UpdateDish", Retrieved);

        }

        [HttpPost("edit/{DishId}")]
        public IActionResult UpdateDish(int DishId, Dish FromForm)
        {
            Dish Retrieved = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
            if(Retrieved == null)
            {
                return RedirectToAction("Index");
            }
            Retrieved.Name = FromForm.Name;
            Retrieved.Chef = FromForm.Chef;
            Retrieved.Tastiness = FromForm.Tastiness;
            Retrieved.Calories = FromForm.Calories;
            Retrieved.Description = FromForm.Description;
            Retrieved.UpdatedAt = FromForm.UpdatedAt;
           /* FromForm.DishId = DishId;
            _context.Update(FromForm);
            _context.Entry(FromForm).Property("CreateAt").IsModified = false; */
            _context.SaveChanges();
            return RedirectToAction("Detail", DishId);
        }

        [HttpGet("{DishId}")]
        public IActionResult Detail(int DishId)
        {
            Dish Retrieved = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
            if(Retrieved == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Detail", Retrieved);
            }
        }

        [HttpGet("delete/{DishId}")]
        public IActionResult DeleteDish(int DishId)
        {
            Dish ToDelete = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
            if(ToDelete == null)
            {
                return RedirectToAction("Index");
            }
            _context.Remove(ToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



           
    }
}
