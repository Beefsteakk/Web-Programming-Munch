using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Models;
using EffectiveWebProg.Data;
using Microsoft.EntityFrameworkCore;
using EffectiveWebProg.ViewModels;

namespace EffectiveWebProg.Controllers;

public class PostsController : Controller
{
    private readonly ApplicationDbContext _db;

    public PostsController(ApplicationDbContext db)
    {
        _db = db;
    }

    private void CompletePostObject(PostsModel post)
    {
        if (post.UserID != null)
        {
            post.User = _db.Users.Find(post.UserID);
        }
        else if (post.RestID != null)
        {
            post.Restaurant = _db.Restaurants.Find(post.RestID);
        }
    }

    private async Task<List<PostsModel>> GetAllPostsAsync()
    {
        var posts = await _db.Posts.OrderByDescending(p => p.PostCreatedAt).ToListAsync();
        foreach (var post in posts)
        {
            CompletePostObject(post);
        }
        return posts;
    }

    private async Task<List<CommentsModel>> GetSpecificPostCommentsAsync(Guid postID)
    {
        var comments = await _db.Comments
            .Where(c => c.PostID == postID)
            .ToListAsync();

        return comments;
    }

    public async Task<IActionResult> Index()
    {
        var userID = Guid.Parse("10bb4451-19aa-11ef-ad56-662ef0370963"); // Temporary variable.
        var posts = await GetAllPostsAsync();
        var postViewModels = new List<PostViewModel>();
        foreach (var post in posts)
        {
            var like = await _db.PostLikesUser.FirstOrDefaultAsync(l => l.PostID == post.PostID && l.UserID == userID);
            var isLiked = like != null;
            postViewModels.Add(new PostViewModel { Post = post, IsLikedByUser = isLiked });
        }
        var viewModel = new MainFeedViewModel
        {
            PostLists = postViewModels,
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePosts(PostsModel post)
    {
        await _db.Posts.AddAsync(post);
        await _db.SaveChangesAsync();
        return RedirectToAction("");
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(CommentsModel comment)
    {
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();
        return RedirectToAction("");
    }

    [HttpPost]
    public async Task<IActionResult> EditPosts(PostsModel post)
    {
        _db.Posts.Update(post);
        await _db.SaveChangesAsync();
        return RedirectToAction("SpecificPost", new { id = post.PostID });
    }

    [HttpPost]
    public async Task<JsonResult> GetInfo(string id)
    {
        var post = await _db.Posts.FindAsync(Guid.Parse(id));
        List<string> ImageUrl = new List<string>();
        ImageUrl.Add("/assets/images/photo-1556008531-57e6eefc7be4.jpeg");
        ImageUrl.Add("/assets/images/photo-1557684387-08927d28c72a.jpeg");
        ImageUrl.Add("/assets/images/photo-1526016650454-68a6f488910a.jpeg");
        return Json(new {imageUrl = ImageUrl, post = post});
    }

    [HttpGet]
    public async Task<IActionResult> SpecificPost(Guid id)
    {
        var post = await _db.Posts.FirstOrDefaultAsync(p => p.PostID == id);
        var comments = await GetSpecificPostCommentsAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        CompletePostObject(post);
        var model = new IndividualPostViewModel { Post = post, CommentsList = comments };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LikePost(Guid postId, Guid userId)
    {
        var like = _db.PostLikesUser.FirstOrDefault(l => l.PostID == postId && l.UserID == userId);
        if (like == null)
        {
            // Add a new like
            var post = await _db.Posts.FirstOrDefaultAsync(p => p.PostID == postId);
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            like = new PostLikesUserModel { Post = post, User = user };
            await _db.PostLikesUser.AddAsync(like);
        }
        else
        {
            _db.PostLikesUser.Remove(like);
        }

        await _db.SaveChangesAsync();
        return RedirectToAction("");
    }
}
