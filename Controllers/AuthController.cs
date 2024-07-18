using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Data;
using Microsoft.EntityFrameworkCore;

namespace EffectiveWebProg.Controllers
{
    public class AuthController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;

        [HttpGet("Auth/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Auth/Register")]
        public async Task<IActionResult> Register(UsersModel user, IFormFile? UserProfilePic)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => string.Equals(u.UserEmail.ToLower(), user.UserEmail.ToLower()));
            if (!ModelState.IsValid || existingUser != null)
            {
                ViewBag.RegisterSuccess = false;
                return View(user);
            }

            if (UserProfilePic != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "UserProfilePics");
                var filename = $"{user.UserID}{Path.GetExtension(UserProfilePic.FileName)}";
                var filePath = Path.Combine(uploadsFolder, filename);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await UserProfilePic.CopyToAsync(fileStream);
                user.UserProfilePic = filename;
            }

            user.UserPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword);
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("Login");
        }

        [HttpGet("Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Auth/Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid || model == null || string.IsNullOrEmpty(model.UserEmail)) return View(model);

            var user = await _db.Users.Where(u => u.UserEmail == model.UserEmail).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.UserPassword != null && BCrypt.Net.BCrypt.Verify(model.UserPassword, user.UserPassword))
                {
                    HttpContext.Session.SetString("SSName", user.UserEmail);
                    HttpContext.Session.SetString("SSID", user.UserID.ToString());
                    HttpContext.Session.SetString("SSUserType", "User");
                    return RedirectToAction("Index", "Posts");
                }
            }

            var restaurant = await _db.Restaurants.Where(r => r.RestEmail == model.UserEmail).FirstOrDefaultAsync();
            if (restaurant != null)
            {
                if (restaurant.RestPassword != null && BCrypt.Net.BCrypt.Verify(model.UserPassword, restaurant.RestPassword))
                {
                    HttpContext.Session.SetString("SSName", restaurant.RestEmail);
                    HttpContext.Session.SetString("SSID", restaurant.RestID.ToString());
                    HttpContext.Session.SetString("SSUserType", "Restaurant");
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            ModelState.AddModelError(string.Empty, "User not found or invalid password.");
            ViewBag.LoginSuccess = false;
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet("Auth/RestaurantReg")]
        public IActionResult RestaurantReg()
        {
            return View();
        }

        [HttpPost("Auth/RestaurantReg")]
        public async Task<IActionResult> RestaurantReg(RestaurantsModel restaurant, IFormFile? RestaurantProfilePic)
        {
            var existingRestaurant = await _db.Restaurants.FirstOrDefaultAsync(r => string.Equals(r.RestEmail.ToLower(), restaurant.RestEmail.ToLower()));
            if (!ModelState.IsValid || existingRestaurant != null)
            {
                ViewBag.RegisterSuccess = false;
                return View(restaurant);
            }

            if (RestaurantProfilePic != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "RestaurantProfilePics");
                var filename = $"{restaurant.RestID}{Path.GetExtension(RestaurantProfilePic.FileName)}";
                var filePath = Path.Combine(uploadsFolder, filename);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await RestaurantProfilePic.CopyToAsync(fileStream);
                restaurant.RestPic = filename;
            }

            restaurant.RestPassword = BCrypt.Net.BCrypt.HashPassword(restaurant.RestPassword);
            await _db.Restaurants.AddAsync(restaurant);
            await _db.SaveChangesAsync();
            return RedirectToAction("Login");
        }

        [HttpGet("Auth/ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
