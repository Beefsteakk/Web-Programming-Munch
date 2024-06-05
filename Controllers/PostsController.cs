using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Models;
using EffectiveWebProg.Data;
using Microsoft.Extensions.ObjectPool;

namespace EffectiveWebProg.Controllers;

public class PostsController : Controller
{
    private ApplicationDbContext _db;

    public PostsController(ApplicationDbContext db)
    {
        _db = db;

    }

    public IActionResult Index(){
        return View();
    }

    public IActionResult Posts(){
        var posts = _db.Posts.ToList();
        return View(posts);
    }

    [HttpGet]
    public IActionResult CreatePosts(){
        return View();
    }

    [HttpPost]
    public IActionResult CreatePosts(PostsModel post){
        _db.Posts.Add(post);
        _db.SaveChanges();

        return RedirectToAction("Posts");
    }

    [HttpPost]
    public JsonResult GetInfo(int id)
    {
        List<string> ImageUrl = new List<string>();
        ImageUrl.Add("/assets/images/photo-1556008531-57e6eefc7be4.jpeg");
        ImageUrl.Add("/assets/images/photo-1557684387-08927d28c72a.jpeg");
        ImageUrl.Add("/assets/images/photo-1526016650454-68a6f488910a.jpeg");
        return Json(new {imageUrl = ImageUrl, info = $"Information for div {id}"});
    }
}