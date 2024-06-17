using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Models;
using EffectiveWebProg.ViewModels;
using EffectiveWebProg.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EffectiveWebProg.Controllers;

public class CategoryController : Controller
{
    private ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var cat = await _db.Category.ToListAsync();
        return View(cat);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CatType")] CategoryModel categoryModel)
    {
        if (ModelState.IsValid)
        {
            categoryModel.CatID = Guid.NewGuid();
            _db.Add(categoryModel);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(categoryModel);
    }

}