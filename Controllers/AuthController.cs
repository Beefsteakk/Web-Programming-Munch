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
        public async Task<IActionResult> Register(UsersModel user)
        {
            if (ModelState.IsValid)
            {
                user.UserPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword);
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            // If we got this far, something failed, redisplay form
            return View(user);
        }

        [HttpGet("Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Auth/Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("hey chimponga");
                // Check if model is null
                if (model == null)
                {
                    Console.WriteLine("Model is null");
                    return View(model);
                }

                // Check if UserEmail is null or empty
                if (string.IsNullOrEmpty(model.UserEmail))
                {
                    Console.WriteLine("UserEmail is null or empty");
                    return View(model);
                }

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
            }

            return View(model);
        }


        // Logout action to clear session
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

            // if (ModelState.IsValid)
            // {
            //     string query = "SELECT UserUsername, UserPassword FROM Users WHERE UserUsername = @UserUsername AND UserPassword = @UserPassword";
            //     // try
            //     {
            //         using (MySqlConnection conn = new MySqlConnection(connectionString))
            //         {
            //             await conn.OpenAsync();
            //             using (MySqlCommand cmd = new MySqlCommand(query, conn))
            //             {
            //                 cmd.Parameters.AddWithValue("@UserUsername", model.UserUsername);
            //                 using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
            //                 {
            //                     if (await reader.ReadAsync())
            //                     {
            //                         string storedPassword = reader.GetString("UserPassword");
            //                         if (BCrypt.Net.BCrypt.Verify(model.UserPassword, storedPassword))
            //                         {
            //                             var claims = new List<Claim>
            //                             {
            //                                 new Claim(ClaimTypes.Name, model.UserUsername)
            //                             };

            //                             var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //                             var authProperties = new AuthenticationProperties
            //                             {
            //                                 IsPersistent = model.RememberMe
            //                             };

            //                             await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            //                             return RedirectToAction("index", "Home");
            //                         }
            //                         else
            //                         {
            //                             ModelState.AddModelError("", "Invalid username or password.");
            //                         }
            //                     }
            //                     else
            //                     {
            //                         ModelState.AddModelError("", "Invalid username or password.");
            //                     }
            //                 }
            //             }
            //         }
            //     }
            //     catch (MySqlException ex)
            //     {
            //         ModelState.AddModelError("", "An error occurred while retrieving data: " + ex.Message);
            //     }
            // }

        //     return View(model);
        // }

        [HttpGet("Auth/RestaurantReg")]
        public IActionResult RestaurantReg()
        {
            return View();
        }

        [HttpGet("Auth/ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
