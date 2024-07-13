using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class CartController : Controller
{
    private readonly ApplicationDbContext _db;

    public CartController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        // Check if the logged-in session is a registered user
        if (HttpContext.Session.GetString("SSUserType") != "Restaurant") return Forbid();

        // Retrieve session ID, which is the restaurant ID
        var sessionID = HttpContext.Session.GetString("SSID");
        if(string.IsNullOrEmpty(sessionID)) return Unauthorized("You must be logged in as a restaurant to view this page.");

        Guid restID;
        if (!Guid.TryParse(sessionID, out restID)) return BadRequest("Invalid session ID.");

        // Fetch the cart linked ot this restaurant
        var cart = await _db.Cart
                            .Include(c => c.CartItem)
                                .ThenInclude(ci => ci.Items)
                            .FirstOrDefaultAsync(c => c.RestID == restID);

        if (cart == null) return View("EmptyCart");
        return View("Index", cart);
    }

}