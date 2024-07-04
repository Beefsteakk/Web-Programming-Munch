using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Models;
using EffectiveWebProg.ViewModels;
using EffectiveWebProg.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EffectiveWebProg.Controllers;

public class ItemCatController : Controller
{
    private ApplicationDbContext _db;

    public ItemCatController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var cat = await _db.ItemCat.ToListAsync();
        return View(cat);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CatName")] ItemCatModel itemCatModel)
    {
        if (ModelState.IsValid)
        {
            itemCatModel.CatID = Guid.NewGuid();
            _db.Add(itemCatModel);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(itemCatModel);
    }

}