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
            var userType = HttpContext.Session.GetString("SSUserType");
            ViewBag.UserType = userType;

            if (userType != "Restaurant")
            {
                return Forbid();
            }

            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to add items to the cart.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

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

            var item = await _db.Items.FindAsync(itemId);
            if (item == null)
            {
                return NotFound($"Item not found. ItemID: {itemId}");
            }

            var cartItem = await _db.CartItems.FirstOrDefaultAsync(ci => ci.CartID == cart.CartID && ci.ItemID == itemId);
            if (cartItem == null)
            {
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
                cartItem.Quantity++;
            }

            cart.CartTotal += item.Price;
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart([FromBody] Guid itemId)
        {
            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to remove items from the cart.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

            var cart = await _db.Cart.FirstOrDefaultAsync(c => c.RestID == restID);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var cartItem = await _db.CartItems
                .Include(ci => ci.Items)
                .FirstOrDefaultAsync(ci => ci.CartID == cart.CartID && ci.ItemID == itemId);
            if (cartItem == null)
            {
                return NotFound("Item not found in cart.");
            }

            _db.CartItems.Remove(cartItem);

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
            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to increase item quantity.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

            var cart = await _db.Cart.FirstOrDefaultAsync(c => c.RestID == restID);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var cartItem = await _db.CartItems
                .Include(ci => ci.Items)
                .FirstOrDefaultAsync(ci => ci.CartID == cart.CartID && ci.ItemID == itemId);
            if (cartItem == null)
            {
                return NotFound("Item not found in cart.");
            }

            cartItem.Quantity++;
            cart.CartTotal += cartItem.Items.Price;

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("DecreaseQuantity")]
        public async Task<IActionResult> DecreaseQuantity([FromBody] Guid itemId)
        {
            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to change item quantities in the cart.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

            var cart = await _db.Cart.FirstOrDefaultAsync(c => c.RestID == restID);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var cartItem = await _db.CartItems
                .Include(ci => ci.Items)
                .FirstOrDefaultAsync(ci => ci.CartID == cart.CartID && ci.ItemID == itemId);
            if (cartItem == null)
            {
                return NotFound("Item not found in cart.");
            }

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

        [HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentData paymentData)
        {
            // Simulate payment validation
            bool paymentSuccess = true;

            if (!paymentSuccess)
            {
                return StatusCode(500, "Payment processing failed.");
            }

            var sessionID = HttpContext.Session.GetString("SSID");
            if (string.IsNullOrEmpty(sessionID))
            {
                return Unauthorized("You must be logged in as a restaurant to complete the purchase.");
            }

            if (!Guid.TryParse(sessionID, out Guid restID))
            {
                return BadRequest("Invalid session ID.");
            }

            var inventory = await _db.Inventory.FirstOrDefaultAsync(i => i.RestID == restID);
            if (inventory == null)
            {
                var restaurant = await _db.Restaurants.FindAsync(restID);
                if (restaurant == null)
                {
                    return NotFound("Restaurant not found.");
                }

                inventory = new InventoryModel
                {
                    InventoryID = Guid.NewGuid(),
                    RestID = restID,
                    InventoryName = "Default Inventory",
                    Rest = restaurant
                };
                _db.Inventory.Add(inventory);
                await _db.SaveChangesAsync();
            }

            var cart = await _db.Cart
                .Include(c => c.CartItem)
                .ThenInclude(ci => ci.Items)
                .FirstOrDefaultAsync(c => c.RestID == restID);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            foreach (var cartItem in cart.CartItem)
            {
                var inventoryItem = await _db.InventoryItems.FirstOrDefaultAsync(ii => ii.ItemID == cartItem.ItemID && ii.InventoryID == inventory.InventoryID);

                if (inventoryItem == null)
                {
                    inventoryItem = new InventoryItemsModel
                    {
                        InventoryID = inventory.InventoryID,
                        ItemID = cartItem.ItemID,
                        StockCount = cartItem.Quantity,
                        TotalPrice = cartItem.Items.Price * cartItem.Quantity,
                        LastUpdated = DateTime.Now,
                        Inventory = inventory,
                        Items = cartItem.Items
                    };
                    _db.InventoryItems.Add(inventoryItem);
                }
                else
                {
                    inventoryItem.StockCount += cartItem.Quantity;
                    inventoryItem.TotalPrice += cartItem.Items.Price * cartItem.Quantity;
                    inventoryItem.LastUpdated = DateTime.Now;
                }
            }

            _db.CartItems.RemoveRange(cart.CartItem);
            cart.CartTotal = 0;

            await _db.SaveChangesAsync();
            return Ok();
        }
    }

    public class PaymentData
    {
        public string PaymentToken { get; set; }
    }
}
