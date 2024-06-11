using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Models;
using EffectiveWebProg.Data;
using Microsoft.EntityFrameworkCore;
using EffectiveWebProg.ViewModels;

namespace EffectiveWebProg.Controllers;

public class PostsController : Controller
{
    private ApplicationDbContext _db;

    public PostsController(ApplicationDbContext db)
    {
        _db = db;
    }

    private async Task<List<PostsModel>> GetAllPostsAsync() {
        var posts = await _db.Posts.OrderByDescending(p => p.PostCreatedAt).ToListAsync();
        foreach (var post in posts) {
            if (post.UserID != null) {
                post.User = await _db.Users.FindAsync(post.UserID);
            } else if (post.RestID != null) {
                post.Restaurant = await _db.Restaurants.FindAsync(post.RestID);
            }
        }
        return posts;
    }

    public async Task<IActionResult> Index() {
        var posts = await GetAllPostsAsync();
        var viewModel = new MainFeedViewModel{
            PostLists = posts,
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePosts(PostsModel post) {
        await _db.Posts.AddAsync(post);
        await _db.SaveChangesAsync();
        return RedirectToAction("");
    }

    [HttpGet]
    public IActionResult EditPosts() {
        return View();
    }

    [HttpPost]
    public IActionResult EditPosts(String postID, String postTitle, String postContent) {
        var post = _db.Posts.Find(System.Guid.Parse(postID));
        if (post != null) {
            post.PostTitle = postTitle;
            post.PostContent = postContent;
            _db.Posts.Update(post);
            _db.SaveChanges();
        }
        return RedirectToAction("Posts");
    }

    [HttpPost]
    public JsonResult GetInfo(String id)
    {
        List<string> ImageUrl = new List<string>();
        ImageUrl.Add("/assets/images/photo-1556008531-57e6eefc7be4.jpeg");
        ImageUrl.Add("/assets/images/photo-1557684387-08927d28c72a.jpeg");
        ImageUrl.Add("/assets/images/photo-1526016650454-68a6f488910a.jpeg");
        var post = _db.Posts.Find(System.Guid.Parse(id));
        return Json(new {imageUrl = ImageUrl, post = post});
    }
}