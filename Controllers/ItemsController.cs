using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using EffectiveWebProg.Data;

namespace EffectiveWebProg.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ItemsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var items = await _db.Items.Include(i => i.ItemCat).ToListAsync();
            return View(items);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _db.Items
                .Include(i => i.ItemCat)
                .FirstOrDefaultAsync(m => m.ItemID == id);

            if (item == null)
            {
                return NotFound();
            }

            ViewBag.CatName = item.ItemCat.CatName;

            return PartialView("_DetailsModal", item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CatID"] = new SelectList(_db.ItemCat, "CatID", "CatName");
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatID,ItemName,ItemPic")] ItemsModel itemsModel)
        {
            if (ModelState.IsValid)
            {
                itemsModel.ItemID = Guid.NewGuid();
                _db.Add(itemsModel);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatID"] = new SelectList(_db.ItemCat, "CatID", "CatName", itemsModel.CatID);
            return View(itemsModel);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsModel = await _db.Items.FindAsync(id);
            if (itemsModel == null)
            {
                return NotFound();
            }

            var itemCat = await _db.ItemCat.FindAsync(itemsModel.CatID);
            ViewBag.CatName = itemCat?.CatName;

            ViewData["CatID"] = new SelectList(_db.ItemCat, "CatID", "CatName", itemsModel.CatID);
            return PartialView("_EditModal", itemsModel);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemID,CatID,ItemName,ItemPic")] ItemsModel itemsModel)
        {
            if (id != itemsModel.ItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(itemsModel);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsModelExists(itemsModel.ItemID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatID"] = new SelectList(_db.ItemCat, "CatID", "CatName", itemsModel.CatID);
            return View(itemsModel);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsModel = await _db.Items
                .Include(i => i.ItemCat)
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (itemsModel == null)
            {
                return NotFound();
            }

            ViewBag.CatName = itemsModel.ItemCat.CatName;

            return PartialView("_DeleteModal", itemsModel);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemsModel = await _db.Items.FindAsync(id);
            _db.Items.Remove(itemsModel);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsModelExists(Guid id)
        {
            return _db.Items.Any(e => e.ItemID == id);
        }
    }
}
