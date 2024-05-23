using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}