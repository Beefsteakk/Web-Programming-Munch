using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
            // Retrieve user type from session and set it to ViewBag
            var userType = HttpContext.Session.GetString("SSUserType");
            ViewBag.UserType = userType;

            if (userType != "Restaurant")
            {
                return Forbid();
            }

            // Retrieve session ID, which is the restaurant ID
            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to view this page.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

            // Fetch the cart linked to this restaurant
            var cart = await _db.Cart
                                .Include(c => c.CartItem)
                                    .ThenInclude(ci => ci.Items)
                                .FirstOrDefaultAsync(c => c.RestID == restID);

            if (cart == null)
            {
                return View("EmptyCart");
            }
            return View("Cart", cart);
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] Guid itemId)
        {
            // Retrieve user type from session and set it to ViewBag
            var userType = HttpContext.Session.GetString("SSUserType");
            ViewBag.UserType = userType;

            if (userType != "Restaurant")
            {
                return Forbid();
            }

            // Retrieve session ID, which is the restaurant ID
            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to add items to the cart.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

            // Find or create a cart for this restaurant
            var cart = await _db.Cart.FirstOrDefaultAsync(c => c.RestID == restID);
            if (cart == null)
            {
                var restaurant = await _db.Restaurants.FindAsync(restID);
                if (restaurant == null)
                {
                    return NotFound("Restaurant not found.");
                }

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
                return NotFound($"Item not found. ItemID: {itemId}");
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

        [HttpPost("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart([FromBody] Guid itemId)
        {
            // Retrieve Restaurant ID from session
            var sessionID = HttpContext.Session.GetString("SSID");  
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to remove items from the cart.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

            // Find the cart
            var cart = await _db.Cart.FirstOrDefaultAsync(c => c.RestID == restID);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            // Find the item
            var cartItem = await _db.CartItems
                .Include(ci => ci.Items) // Ensure Items navigation property is included
                .FirstOrDefaultAsync(ci => ci.CartID == cart.CartID && ci.ItemID == itemId);
            if (cartItem == null)
            {
                return NotFound("Item not found in cart.");
            }

            // Remove the item
            _db.CartItems.Remove(cartItem);

            // Update cart total if Items is not null
            if (cartItem.Items != null)
            {
                cart.CartTotal -= cartItem.Items.Price * cartItem.Quantity;
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("IncreaseQuantity")]
        public async Task<IActionResult> IncreaseQuantity([FromBody] Guid itemId)
        {
            // Retrieve Restaurant ID from session
            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to increase item quantity.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

            // Find the cart
            var cart = await _db.Cart.FirstOrDefaultAsync(c => c.RestID == restID);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            // Find the item
            var cartItem = await _db.CartItems
                .Include(ci => ci.Items)
                .FirstOrDefaultAsync(ci => ci.CartID == cart.CartID && ci.ItemID == itemId);
            if (cartItem == null)
            {
                return NotFound("Item not found in cart.");
            }

            // Increase quanity of the item
            cartItem.Quantity++;
            cart.CartTotal += cartItem.Items.Price;

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("DecreaseQuantity")]
        public async Task<IActionResult> DecreaseQuantity([FromBody] Guid itemId)
        {
            // Retrieve Restaurant ID from session
            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to change item quantities in the cart.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

            // Find the cart
            var cart = await _db.Cart.FirstOrDefaultAsync(c => c.RestID == restID);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            // Find the item
            var cartItem = await _db.CartItems
                .Include(ci => ci.Items)
                .FirstOrDefaultAsync(ci => ci.CartID == cart.CartID && ci.ItemID == itemId);
            if (cartItem == null)
            {
                return NotFound("Item not found in cart.");
            }

            // Decrease quantity of existing item
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                cart.CartTotal -= cartItem.Items.Price;
            }
            else
            {
                _db.CartItems.Remove(cartItem);
                cart.CartTotal -= cartItem.Items.Price * cartItem.Quantity;
            }

            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
