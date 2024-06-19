using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Models;
using EffectiveWebProg.Data;
using Microsoft.EntityFrameworkCore;
using EffectiveWebProg.ViewModels;
using EffectiveWebProg.DTOs.Posts;

namespace EffectiveWebProg.Controllers;

public class PostsController : BaseController
{
    private readonly ApplicationDbContext _db;

    public PostsController(ApplicationDbContext db)
    {
        _db = db;
    }

    private async Task<List<CommentsModel>> GetSpecificPostCommentsAsync(Guid postID)
    {
        var comments = await _db.Comments
            .OrderBy(c => c.CommentCreatedAt)
            .Where(c => c.PostID == postID)
            .ToListAsync();

        return comments;
    }

    public async Task<IActionResult> Index()
    {
        var sessionID = HttpContext.Session.GetString("SSID") ?? "";
        var posts = await FetchUserViewablePosts();
        var postViewModels = new List<PostViewModel>();
        foreach (var post in posts)
        {
            var like = await _db.PostLikesUser.FirstOrDefaultAsync(
                l => l.PostID == post.PostID && l.UserID == Guid.Parse(sessionID)
            );
            postViewModels.Add(new PostViewModel { Post = post, IsLikedByUser = like != null });
        }
        ViewBag.UserID = sessionID;
        return View(new MainFeedViewModel(postViewModels));
    }

    /// <summary>
    /// Returns a list of posts that the logged in user can see. (This means the user's own posts,
    /// and the posts of other users that are being followed by the currently logged in user.)
    /// </summary>
    public async Task<List<PostsModel>> FetchUserViewablePosts()
    {
        // TODO: Implement logic for restaurants too.
        var sessionID = HttpContext.Session.GetString("SSID");
        if (sessionID == null)
        {
            return new List<PostsModel>();
        }

        var posts = await _db.Posts.FromSqlRaw(
            "SELECT Posts.* FROM Posts WHERE Posts.UserID = {0} " +
            "UNION " +
            "SELECT Posts.* FROM Posts JOIN UserFollowings ON Posts.UserID = UserFollowings.FollowedUserID " +
            "WHERE UserFollowings.UserID = {0}",
            sessionID
        ).OrderByDescending(p => p.PostCreatedAt).ToListAsync();

        foreach (var post in posts)
        {
            CompletePostObject(post);
        }
        return posts;
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
        var Gid = Guid.Parse(id);
        var post = await _db.Posts.FirstOrDefaultAsync(p => p.PostID == Gid);
        if (post == null)
        {
            return Json(new { success = false });
        }
        CompletePostObject(post);

        var postDTO = new PostDTO(post.PostID, new PostAuthorDTO(post.User.UserID, post.User.UserUsername), post.PostContent, post.PostCreatedAt.ToString("G"));
        var comments = await GetSpecificPostCommentsAsync(Gid);
        var commentDTOList = new List<CommentDTO>();
        foreach (var comment in comments)
        {
            var author = await _db.Users.FirstOrDefaultAsync(u => u.UserID == comment.UserID);
            commentDTOList.Add(new CommentDTO(comment.CommentID, comment.PostID, new CommentAuthorDTO(author.UserID, author.UserUsername), comment.CommentContent));
        }
        List<string> ImageUrl = new List<string>();
        ImageUrl.Add("/assets/images/photo-1556008531-57e6eefc7be4.jpeg");
        ImageUrl.Add("/assets/images/photo-1557684387-08927d28c72a.jpeg");
        ImageUrl.Add("/assets/images/photo-1526016650454-68a6f488910a.jpeg");
        return Json(new { imageUrl = ImageUrl, post = postDTO, comments = commentDTOList, success = true });
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
}
