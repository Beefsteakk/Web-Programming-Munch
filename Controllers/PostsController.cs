using Microsoft.AspNetCore.Mvc;
using EffectiveWebProg.Models;
using EffectiveWebProg.Data;
using Microsoft.EntityFrameworkCore;
using EffectiveWebProg.ViewModels;
using EffectiveWebProg.DTOs.Posts;

namespace EffectiveWebProg.Controllers;

public class PostsController(ApplicationDbContext db) : BaseController
{
    private readonly ApplicationDbContext _db = db;

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
        var posts = await FetchViewablePostsByIDAsync(Guid.Parse(sessionID));
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
        await CompletePostObject(post);

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
        await CompletePostObject(post);
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

    [HttpGet]
    public async Task<JsonResult> SearchRestaurant(string term) {
        var restaurants = await _db.Restaurants
            .Where(r => r.RestName.Contains(term))
            .Select(r => new { r.RestID, r.RestName })
            .ToListAsync();

        return Json(restaurants);
    }

    /// <summary>
    /// Returns a list of posts that the logged in user can see. (This means the user's own posts,
    /// and the posts of other users that are being followed by the currently logged in user.)
    /// </summary>
    private async Task<List<PostsModel>> FetchViewablePostsByIDAsync(Guid id)
    {
        var posts = new List<PostsModel>();
        var restaurant = await _db.Restaurants.FirstOrDefaultAsync(r => r.RestID == id);
        if (restaurant != null)
        {
            posts = await _db.Posts.FromSqlRaw(
                "SELECT Posts.* FROM Posts WHERE Posts.RestID = {0}", id
            ).OrderByDescending(p => p.PostCreatedAt).ToListAsync();
        }
        else
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null) return posts;
            posts = await _db.Posts.FromSqlRaw(
                "SELECT Posts.* FROM Posts WHERE Posts.UserID = {0} " +
                "UNION " +
                "SELECT Posts.* FROM Posts JOIN UserFollowings ON Posts.UserID = UserFollowings.FollowedUserID " +
                "WHERE UserFollowings.UserID = {0} " +
                "UNION " +
                "SELECT Posts.* FROM Posts JOIN RestaurantFollowings ON Posts.RestID = RestaurantFollowings.FollowedRestID " +
                "WHERE RestaurantFollowings.UserID = {0} ",
                user.UserID
            ).OrderByDescending(p => p.PostCreatedAt).ToListAsync();
        }

        foreach (var post in posts)
        {
            await CompletePostObject(post);
        }
        return posts;
    }

    private async Task CompletePostObject(PostsModel post)
    {
        if (post.UserID != null)
        {
            post.User = await _db.Users.FindAsync(post.UserID);
        }
        else if (post.RestID != null)
        {
            post.Restaurant = await _db.Restaurants.FindAsync(post.RestID);
        }

        /* This is commented out because the below information are unused for now and adds a considerable loading time.
        if (post.TaggedRest != null)
        {
            post.TaggedRestaurant = await _db.Restaurants.FindAsync(post.TaggedRest);
        }

        post.Comment = await _db.Comments.FromSqlRaw(
            "SELECT * FROM Comments WHERE PostID = {0}",
            post.PostID
        ).OrderByDescending(c => c.CommentCreatedAt).ToListAsync();

        post.PostLikeUser = await _db.PostLikesUser.FromSqlRaw(
            "SELECT * FROM PostLikesUser WHERE PostID = {0}",
            post.PostID
        ).ToListAsync();

        post.PostLikeRest = await _db.PostLikesRest.FromSqlRaw(
            "SELECT * FROM PostLikesRest WHERE PostID = {0}",
            post.PostID
        ).ToListAsync();

        post.Review = await _db.Reviews.FromSqlRaw(
            "SELECT * FROM Reviews WHERE PostID = {0}",
            post.PostID
        ).ToListAsync();

        post.PostPic = await _db.PostPics.FromSqlRaw(
            "SELECT * FROM PostPics WHERE PostID = {0}",
            post.PostID
        ).ToListAsync();
        */
    }
}
