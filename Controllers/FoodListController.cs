using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Models;
using EffectiveWebProg.ViewModels;
using EffectiveWebProg.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EffectiveWebProg.Controllers;

public class FoodListController : Controller
{
    private ApplicationDbContext _db;

    public FoodListController(ApplicationDbContext db)
    {
        _db = db;
    }

    private Guid? GetUserIdFromSession()
    {
        string sessionEmail = HttpContext.Session.GetString("SSName");

        if (string.IsNullOrEmpty(sessionEmail))
        {
            return null;
        }

        var user = _db.Users.SingleOrDefault(u => u.UserEmail == sessionEmail);
        return user?.UserID;
    }

    public IActionResult Index(string searchQuery = "")
    {
        var userId = GetUserIdFromSession();

        if (userId == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        var foodlists = string.IsNullOrEmpty(searchQuery)
            ? _db.FoodLists.Where(fl => fl.UserID == userId).ToList() // Filter food lists based on UserID
            : _db.FoodLists.Where(fl => fl.UserID == userId && fl.FoodListTitle.Contains(searchQuery)).ToList();

        return View(foodlists);
    }



    [HttpGet]
    public IActionResult CreateFoodLists(){

        return PartialView("_CreateFoodLists");
    }

    [HttpPost]
    public IActionResult CreateFoodLists(FoodListsModel foodlist, IFormFile? foodListImage){
        
        var userId = GetUserIdFromSession();

        if (userId == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        foodlist.UserID = userId.Value;
        
        if (foodListImage != null)
        {
            var imagePath = Path.Combine("wwwroot/Images", foodListImage.FileName);  
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                foodListImage.CopyTo(stream);
            }
            foodlist.FoodListImage = $"/Images/{foodListImage.FileName}";
        }

        _db.FoodLists.Add(foodlist);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult ViewFoodList(Guid id)
    {
        var foodlist = _db.FoodLists
            .Include(f => f.User)
            .Include(f => f.FoodListEntry)
            .ThenInclude(fle => fle.Restaurant)
            .FirstOrDefault(f => f.FoodListID == id);
        
        if (foodlist == null) return NotFound();

        var restaurants = _db.Restaurants.Select(r => new {r.RestID, r.RestName}).ToList();
        var viewModel = new FoodListViewModel
        {
            FoodList = foodlist,
            FoodListEntries = foodlist.FoodListEntry.ToList(),
            Restaurants = new SelectList(restaurants, "RestID", "RestName")
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult AddRestaurantToFoodList(Guid foodListId, Guid restaurantId)
    {
        var foodList = _db.FoodLists.Find(foodListId);
        var restaurant = _db.Restaurants.Find(restaurantId);

        if (foodList == null || restaurant == null)
        {
            return NotFound();
        }

        var entry = new FoodListEntriesModel
        {
            FoodListID = foodListId,
            RestID = restaurantId,
            FoodList = foodList,
            Restaurant = restaurant
        };

        _db.FoodListEntries.Add(entry);
        _db.SaveChanges();

        return RedirectToAction("ViewFoodList", new {id = foodListId});
    }

    [HttpPost]
    public IActionResult DeleteRestaurantFromFoodList(Guid foodListId, Guid restaurantId)
    {
        var entry = _db.FoodListEntries
            .FirstOrDefault(e => e.FoodListID == foodListId && e.RestID == restaurantId);

        if (entry == null)
        {
            return NotFound();
        }

        _db.FoodListEntries.Remove(entry);
        _db.SaveChanges();

        return RedirectToAction("ViewFoodList", new { id = foodListId });
    }
}