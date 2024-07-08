using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using EffectiveWebProg.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EffectiveWebProg.Controllers
{
    public class ShopController : BaseController
    {
        private readonly ApplicationDbContext _db;

        public ShopController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Shop
        public async Task<IActionResult> Index()
        {
            var items = await _db.Items.Include(i => i.ItemCat).ToListAsync();
            var categories = await _db.ItemCat.ToListAsync();
            var viewModel = new ShopViewModel
            {
                Item = items,
                ItemCat = categories
            };
            return View(viewModel);
        }
    }
}