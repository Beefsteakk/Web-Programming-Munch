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

    public IActionResult Index(){
        var foodlists = _db.FoodLists.ToList();
        return View(foodlists);
    }

    [HttpGet]
    public IActionResult CreateFoodLists(){
        // Fetch users from the database
        var users = _db.Users.Select(u => new {u.UserID, u.UserName}).ToList();

        // Pass the user list to the view via a ViewBag
        ViewBag.Users = new SelectList(users, "UserID", "UserName");

        return PartialView("_CreateFoodLists");
    }

    [HttpPost]
    public IActionResult CreateFoodLists(FoodListsModel foodlist){
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
}