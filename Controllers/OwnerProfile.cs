using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class OwnerProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}