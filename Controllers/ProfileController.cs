using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EffectiveWebProg.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}