using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using EffectiveWebProg.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EffectiveWebProg.Controllers
{
    public class InventoryController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private const int PageSize = 10;

        public InventoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Items
        public async Task<IActionResult> Index(string searchString, Guid? categoryFilter, int page = 1)
        {
            var userType = HttpContext.Session.GetString("SSUserType");
            ViewBag.UserType = userType;

            if (userType != "Restaurant")
            {
                return Forbid();
            }

            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to view this page.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

            var query = _db.InventoryItems
                .Include(ii => ii.Items)
                .Include(ii => ii.Inventory)
                .Where(ii => ii.Inventory.RestID == restID)
                .AsQueryable();

            // if (!string.IsNullOrEmpty(searchString))
            // {
            //     query = query.Where(ii => ii.Items.ItemName.Contains(searchString));
            // }

            // if (categoryFilter.HasValue)
            // {
            //     query = query.Where(ii => ii.Items.CatID == categoryFilter.Value);
            // }

            var totalItems = await query.CountAsync();
            var inventoryItems = await query
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var categories = await _db.ItemCat.ToListAsync();
            var items = await _db.Items.Include(i => i.ItemCat).ToListAsync();

            // ViewBag.CurrentSearch = searchString;
            // ViewBag.CurrentCategory = categoryFilter;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            ViewBag.TotalStockCount = query.Sum(ii => ii.StockCount);
            ViewBag.TotalPrice = query.Sum(ii => ii.TotalPrice);
            ViewBag.TotalItems = query.Count();

            var viewModel = new InventoryViewModel
            {
                InventoryItems = inventoryItems,
                ItemCat = categories
            };
            return View(viewModel);
        }
    }
}
