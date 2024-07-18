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

        public InventoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Items
        public async Task<IActionResult> Index()
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

            var inventoryItems = await query.ToListAsync();
            var categories = await _db.ItemCat.ToListAsync();

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

        [HttpPost]
        public async Task<IActionResult> DecreaseStock(Guid inventoryId, Guid itemId)
        {
            var inventoryItem = await _db.InventoryItems
                .FirstOrDefaultAsync(ii => ii.InventoryID == inventoryId && ii.ItemID == itemId);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            if (inventoryItem.StockCount > 0)
            {
                inventoryItem.StockCount--;
                await _db.SaveChangesAsync();
            }

            return Ok(new { inventoryId, itemId, newStockCount = inventoryItem.StockCount });
        }
    }
}
