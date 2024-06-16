using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EffectiveWebProg.Controllers
{
    public class AuthController : Controller
    {
        private readonly string connectionString = "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";

        // GET: /Auth/Register
        [HttpGet("Auth/Register")]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost("Auth/Register")]
        public async Task<IActionResult> Register(UsersModel user)
        {
            if (ModelState.IsValid)
            {
                string query = "INSERT INTO Users (UserID, UserName, UserEmail, UserContactNum, UserUsername, UserPassword, UserProfilePic, UserBio, AccountToken, AccountVerified, UserCreatedAt) " +
                               "VALUES (@UserID, @UserName, @UserEmail, @UserContactNum, @UserUsername, @UserPassword, @UserProfilePic, @UserBio, @AccountToken, @AccountVerified, @UserCreatedAt)";

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        await conn.OpenAsync();
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", user.UserID);
                            cmd.Parameters.AddWithValue("@UserName", user.UserName);
                            cmd.Parameters.AddWithValue("@UserEmail", user.UserEmail);
                            cmd.Parameters.AddWithValue("@UserContactNum", user.UserContactNum);
                            cmd.Parameters.AddWithValue("@UserUsername", user.UserUsername);
                            cmd.Parameters.AddWithValue("@UserPassword", BCrypt.Net.BCrypt.HashPassword(user.UserPassword)); // Hash the password
                            cmd.Parameters.AddWithValue("@UserProfilePic", user.UserProfilePic ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@UserBio", user.UserBio ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@AccountToken", user.AccountToken ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@AccountVerified", user.AccountVerified ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@UserCreatedAt", user.UserCreatedAt);

                            await cmd.ExecuteNonQueryAsync();
                        }
                    }

                    return RedirectToAction("Login");
                }
                catch (MySqlException ex)
                {
                    // Log the error (you can use a logging framework like Serilog, NLog, etc.)
                    ModelState.AddModelError("", "An error occurred while saving data: " + ex.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(user);
        }

        // GET: /Auth/Login
        [HttpGet("Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost("Auth/Login")]

        public IActionResult Login(LoginModel model)
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

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Modify the query to select the hashed password using UserEmail
                        using (MySqlCommand cmd = new MySqlCommand(
                            "SELECT UserPassword AS Password, UserEmail AS Email " +
                            "FROM Users " +
                            "WHERE UserEmail = @UserEmail " +
                            "UNION " +
                            "SELECT RestPassword AS Password, RestEmail AS Email " +
                            "FROM Restaurants " +
                            "WHERE RestEmail = @UserEmail", conn))
                            {
                            cmd.Parameters.AddWithValue("@UserEmail", model.UserEmail);

                            string storedPassword = cmd.ExecuteScalar()?.ToString();

                            if (storedPassword != null && BCrypt.Net.BCrypt.Verify(model.UserPassword, storedPassword))
                            {
                                Console.WriteLine($"Debugging works! UserEmail: {model.UserEmail}");
                                Console.WriteLine($"Debugging works! Password: {model.UserPassword}");

                                HttpContext.Session.SetString("SSName", model.UserEmail);

                                // Redirect to Home Index page on successful login
                                return RedirectToAction("Index", "Posts");

                                // HttpContext.Session.SetString("user", currentUser.Trim());
                                // _svc.retrieveuserid(HttpContext.Session.GetString("user"));

                                                    // Create session



                            }
                            else
                            {
                                Console.WriteLine("Error, user not found or invalid password!");
                                ModelState.AddModelError(string.Empty, "User not found or invalid password.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception as needed
                        Console.WriteLine($"Exception: {ex.Message}");
                        ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                    }
                }
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

        // GET: /Auth/RestaurantReg
        [HttpGet("Auth/RestaurantReg")]
        public IActionResult RestaurantReg()
        {
            return View();
        }

        // GET: /Auth/ForgotPassword
        [HttpGet("Auth/ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
