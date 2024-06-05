using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class AuthController : Controller
    {
        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // Optionally, you can add actions for registration, etc.
        // GET: /Auth/Register
        public IActionResult Register()
        {
            return View();
        }

        // GET: /Auth/RestaurantReg
        public IActionResult RestaurantReg()
        {
            return View();
        }

        // GET: /Auth/ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
