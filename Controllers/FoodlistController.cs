using Microsoft.AspNetCore.Mvc;

namespace EffectiveWebProg.Controllers
{
    public class FoodlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}