using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using EffectiveWebProg.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var inventoryItems = await _db.InventoryItems
                .Include(ii => ii.Items)
                .Include(ii => ii.Inventory)
                .Where(ii => ii.Inventory.RestID == restID)
                .ToListAsync();

            return View(inventoryItems);
        }       
    }
}