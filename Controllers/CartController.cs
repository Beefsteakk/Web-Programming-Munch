using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EffectiveWebProg.Controllers
{
    [Route("Cart")]
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
            return View("Cart", cart);
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] Guid itemId)
        {
            // Check if the logged-in session is a registered restaurant
            if (HttpContext.Session.GetString("SSUserType") != "Restaurant")
                return Forbid();

            // Retrieve session ID, which is the restaurant ID
            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
                return Unauthorized("You must be logged in as a restaurant to add items to the cart.");

            if (!Guid.TryParse(sessionID, out Guid restID))
                return BadRequest("Invalid session ID.");

            // Find or create a cart for this restaurant
            var cart = await _db.Cart.FirstOrDefaultAsync(c => c.RestID == restID);
            if (cart == null)
            {
                var restaurant = await _db.Restaurants.FindAsync(restID);
                if (restaurant == null)
                    return NotFound("Restaurant not found.");

                cart = new CartModel
                {
                    RestID = restID,
                    Status = "Active",
                    CartName = "Default Cart",
                    Rest = restaurant
                };
                _db.Cart.Add(cart);
                await _db.SaveChangesAsync();
            }

            // Find the item
            var item = await _db.Items.FindAsync(itemId);
            if (item == null)
            {
                return NotFound("Item not found. ItemID: {itemId}. Sample item in DB: ID = {firstItem?.ItemID}, Name = {firstItem?.ItemName}");
            }

            // Check if the item is already in the cart
            var cartItem = await _db.CartItems.FirstOrDefaultAsync(ci => ci.CartID == cart.CartID && ci.ItemID == itemId);
            if (cartItem == null)
            {
                // Add new item to cart
                cartItem = new CartItemsModel
                {
                    CartID = cart.CartID,
                    ItemID = itemId,
                    Quantity = 1,
                    Cart = cart,
                    Items = item
                };
                _db.CartItems.Add(cartItem);
            }
            else
            {
                // Increase quantity of existing item
                cartItem.Quantity++;
            }

            // Update cart total
            cart.CartTotal += item.Price;

            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}
