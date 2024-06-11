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

    private void CompletePostObject(PostsModel post) {
        if (post.UserID != null) {
            post.User = _db.Users.Find(post.UserID);
        } else if (post.RestID != null) {
            post.Restaurant = _db.Restaurants.Find(post.RestID);
        }
    }
    private async Task<List<PostsModel>> GetAllPostsAsync() {
        var posts = await _db.Posts.OrderByDescending(p => p.PostCreatedAt).ToListAsync();
        foreach (var post in posts) {
            CompletePostObject(post);
        }
        return posts;
    }

    private async Task<List<CommentsModel>> GetSpecificPostCommentsAsync(Guid postID) {
        var comments = await _db.Comments
            .Where(c => c.PostID == postID)
            .ToListAsync();

        return comments;
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

    [HttpPost]
    public async Task<IActionResult> AddComment(CommentsModel comment) {
        await _db.Comments.AddAsync(comment);
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
    async public Task<JsonResult> GetInfo(string id)
    {
        var post = await _db.Posts.FindAsync(Guid.Parse(id));
        List<string> ImageUrl = new List<string>();
        ImageUrl.Add("/assets/images/photo-1556008531-57e6eefc7be4.jpeg");
        ImageUrl.Add("/assets/images/photo-1557684387-08927d28c72a.jpeg");
        ImageUrl.Add("/assets/images/photo-1526016650454-68a6f488910a.jpeg");
        return Json(new {imageUrl = ImageUrl, title = $"{post.PostTitle}", message = $"{post.PostContent}"});
    }

    [HttpGet]
    public async Task<IActionResult> SpecificPost(Guid id) {
        Console.WriteLine(id.ToString());
        var post = await _db.Posts.FirstOrDefaultAsync(p => p.PostID == id);
        var comments = await GetSpecificPostCommentsAsync(id);
        if (post == null) {
            return NotFound();
        }
        CompletePostObject(post);
        var model = new IndividualPostViewModel{Post=post, CommentsList=comments};
        return View(model);
    }
}
