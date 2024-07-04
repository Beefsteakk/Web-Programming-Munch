using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using EffectiveWebProg.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EffectiveWebProg.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ItemController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var items = await _db.Items.Include(i => i.ItemCat).ToListAsync();
            if (items == null || !items.Any())
            {
                Console.WriteLine("No items found in the database.");
            }
            return View(items);
        }

        // GET: Shop/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _db.ItemCat.ToListAsync();
            var viewModel = new ItemCreateViewModel
            {
                Categories = categories
            };
            return View(viewModel);
        }

        // POST: Shop/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = await _db.ItemCat.FindAsync(model.CatID);
                if (category == null)
                {
                    ModelState.AddModelError("CatID", "Invalid category ID.");
                    model.Categories = await _db.ItemCat.ToListAsync();
                    return View(model);
                }

                // Save the uploaded file and get the file path
                var itemPicPath = await SaveFile(model.ItemPic);
                
                var item = new ItemsModel
                {
                    ItemID = Guid.NewGuid(),
                    CatID = model.CatID,
                    ItemName = model.ItemName,
                    ItemPic = itemPicPath,
                    ItemCat = category // Set the ItemCat property
                };

                _db.Items.Add(item);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            model.Categories = await _db.ItemCat.ToListAsync();
            return View(model);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{file.FileName}";
        }
    }
}