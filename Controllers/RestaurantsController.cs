using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EffectiveWebProg.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            return View(restaurants);
        }
    }
}
