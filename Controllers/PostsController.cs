using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Models;
using EffectiveWebProg.Data;

namespace EffectiveWebProg.Controllers;

public class PostsController : Controller
{
    private ApplicationDbContext _db;

    public PostsController(ApplicationDbContext db)
    {
        _db = db;

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
    public IActionResult CreatePosts(PostsEntity post){
        _db.Posts.Add(post);
        _db.SaveChanges();

        return RedirectToAction("Posts");
    }
}