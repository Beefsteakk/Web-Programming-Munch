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

    public async Task<IActionResult> Index()
    {
        if (HttpContext.Session.GetString("SSUserType") != "User") return Forbid();
        var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
        var posts = await FetchViewablePostsByIDAsync(sessionID);
        var postViewModels = new List<PostViewModel>();
        foreach (var post in posts)
        {
            var imageURLs = await _db.PostPics
                .Where(p => p.PostID == post.PostID)
                .Select(p => p.ImageURL)
                .ToListAsync();

            var user = await _db.Users.FindAsync(sessionID);
            if (user != null){
                var like = await _db.PostLikes.FirstOrDefaultAsync(
                    l => l.PostID == post.PostID && l.UserID == user.UserID
                );
                postViewModels.Add(new PostViewModel(post, like != null, false, imageURLs));
            }
        }
        return View(new MainFeedViewModel(postViewModels));
    }

    [HttpPost]
    [Route("Posts/CreatePost")]
    public async Task<IActionResult> CreatePostAsync(string postContent, List<IFormFile> pictures)
    {
        if (HttpContext.Session.GetString("SSUserType") != "Restaurant") return Forbid();
        var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
        var restaurant = await _db.Restaurants.FindAsync(sessionID);
        if (restaurant == null) return Forbid();

        var post = new PostsModel() { PostContent = postContent };
        if (pictures.Count > 0) {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "PostPics");
            foreach (var picture in pictures) {
                var postPicture = new PostPicsModel(picture.FileName) {PostID = post.PostID};
                var filePath = Path.Combine(uploadsFolder, postPicture.ImageURL);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await picture.CopyToAsync(fileStream);
                await _db.PostPics.AddAsync(postPicture);
            }
        }
    
        post.RestID = restaurant.RestID;
        await _db.Posts.AddAsync(post);
        await _db.SaveChangesAsync();
        return RedirectToAction("");
    }

    [HttpPost]
    [Route("Posts/EditPost")]
    public async Task<IActionResult> EditPostByIDAsync(Guid postID, string content)
    {
        var post = await FetchPostByIDAsync(postID);
        if (post == null) return NotFound();
        if (!CheckOperationIsPermitted(post)) return Forbid();

        post.PostContent = content;
        _db.Posts.Update(post);
        await _db.SaveChangesAsync();
        return RedirectToAction("SpecificPost", new { id = post.PostID });
    }

    [HttpPost]
    [Route("Posts/DeletePost")]
    public async Task<IActionResult> DeletePostByIDAsync(Guid postID)
    {
        var post = await FetchPostByIDAsync(postID);
        if (post == null) return NotFound();
        if (!CheckOperationIsPermitted(post)) return Forbid();

        var pictures = await _db.PostPics.Where(p => p.PostID == postID).ToListAsync();
        if (pictures != null && pictures.Count > 0) {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "PostPics");
            foreach (var picture in pictures) {
                var filePath = Path.Combine(uploadsFolder, picture.ImageURL);
                System.IO.File.Delete(filePath);
            }
        }
        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();
        return RedirectToAction("");
    }

    [HttpPost]
    [Route("Posts/AddComment")]
    public async Task<IActionResult> AddCommentAsync(CommentsModel comment)
    {
        var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
        var user = await _db.Users.FindAsync(sessionID);
        if (user != null)
        {
            comment.UserID = user.UserID;
        }
        else
        {
            var restaurant = await _db.Restaurants.FindAsync(sessionID);
            if (restaurant == null) return Forbid();

            comment.RestID = restaurant.RestID;
        }
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();
        return RedirectToAction("");
    }

    [HttpPost]
    [Route("Posts/EditComment")]
    public async Task<IActionResult> EditCommentByIDAsync(Guid commentID, string content)
    {
        var comment = await FetchCommentByIDAsync(commentID);
        if (comment == null) return NotFound();
        if (!CheckCommentOperationIsPermitted(comment)) return Forbid();

        comment.CommentContent = content;
        _db.Comments.Update(comment);
        await _db.SaveChangesAsync();
        return RedirectToAction("SpecificPost", new { id = comment.PostID });
    }

    [HttpPost]
    [Route("Posts/DeleteComment")]
    public async Task<IActionResult> DeleteCommentByIDAsync(Guid commentID)
    {
        var comment = await FetchCommentByIDAsync(commentID);
        if (comment == null) return NotFound();
        if (!CheckCommentOperationIsPermitted(comment)) return Forbid();

        _db.Comments.Remove(comment);
        await _db.SaveChangesAsync();
        return RedirectToAction("");
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

        var postDTO = new PostDTO(post.PostID, post.PostContent, post.PostCreatedAt.ToString("G"));
        if (post.Restaurant != null)
        {
            postDTO.PostAuthorRestaurant = new PostRestaurantAuthorDTO(post.Restaurant.RestID, post.Restaurant.RestName);
        }

        var comments = await GetSpecificPostCommentsAsync(Gid);
        var commentDTOList = new List<CommentDTO>();
        foreach (var comment in comments)
        {
            var commentDTO = new CommentDTO(comment.CommentID, comment.PostID, comment.CommentContent);
            if (comment.UserID != null)
            {
                var author = await _db.Users.FirstOrDefaultAsync(u => u.UserID == comment.UserID);
                commentDTO.CommentAuthorUser = new CommentUserAuthorDTO(author.UserID, author.UserUsername);
            }
            if (comment.RestID != null)
            {
                var author = await _db.Restaurants.FirstOrDefaultAsync(r => r.RestID == comment.RestID);
                commentDTO.CommentAuthorRestaurant = new CommentRestaurantAuthorDTO(author.RestID, author.RestName);
            }
            commentDTOList.Add(commentDTO);
        }

        var imageURLs = await _db.PostPics
            .Where(p => p.PostID == Gid)
            .Select(p => p.ImageURL)
            .ToListAsync();

        if (imageURLs != null)
        {
            postDTO.PostPictureURLs = imageURLs;
        }

        return Json(new { post = postDTO, comments = commentDTOList, success = true });
    }

    [HttpGet]
    public async Task<IActionResult> SpecificPost(Guid id)
    {
        var post = await FetchPostByIDAsync(id);
        if (post == null) return NotFound();
        if (!CheckOperationIsPermitted(post)) return Forbid();

        var comments = await GetSpecificPostCommentsAsync(id);
        await CompletePostObject(post);
        var imageURLs = await _db.PostPics
            .Where(p => p.PostID == id)
            .Select(p => p.ImageURL)
            .ToListAsync();

        var model = new IndividualPostViewModel { Post = post, CommentsList = comments, PostImageURLs = imageURLs };
        return View(model);
    }

    [HttpPost]
    [Route("Posts/LikePost")]
    public async Task<JsonResult> LikePostAsync(Guid postId)
    {
        var post = await FetchPostByIDAsync(postId);
        var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
        if (post == null) return Json(new {status = "failed", reason = "Post not found."});

        var user = await _db.Users.FindAsync(sessionID);
        if (user == null) return Json(new {status = "failed", reason = "User not found."});
    
        var like = await _db.PostLikes.FirstOrDefaultAsync(l => l.PostID == postId && l.UserID == sessionID);
        if (like == null)
        {
            like = new PostLikesModel { Post = post, User = user };
            await _db.PostLikes.AddAsync(like);
        }
        else
        {
            _db.PostLikes.Remove(like);
        }

        await _db.SaveChangesAsync();
        return Json(new {status = "success"});
    }

    [HttpGet]
    public async Task<JsonResult> SearchRestaurant(string term)
    {
        var restaurants = await _db.Restaurants
            .Where(r => r.RestName.Contains(term))
            .Select(r => new { r.RestID, r.RestName })
            .ToListAsync();

        return Json(restaurants);
    }

    private bool CheckOperationIsPermitted(PostsModel post)
    {
        var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
        return post.RestID == sessionID;
    }

    private bool CheckCommentOperationIsPermitted(CommentsModel comment)
    {
        var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
        if (HttpContext.Session.GetString("SSUserType") == "Restaurant")
        {
            if (comment.Restaurant == null) return false;
            return comment.Restaurant.RestID == sessionID;
        }
        else
        {
            if (comment.User == null) return false;
            return comment.User.UserID == sessionID;
        }
    }

    private async Task<List<PostsModel>> FetchViewablePostsByIDAsync(Guid id)
    {
        var posts = new List<PostsModel>();
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
        if (user == null) return posts;
        posts = await _db.Posts.FromSqlRaw(
            "SELECT Posts.* FROM Posts JOIN Followings ON Posts.RestID = Followings.RestID " +
            "WHERE Followings.UserID = {0} ",
            user.UserID
        ).OrderByDescending(p => p.PostCreatedAt).ToListAsync();

        foreach (var post in posts)
        {
            await CompletePostObject(post);
        }
        return posts;
    }

    private async Task<PostsModel?> FetchPostByIDAsync(Guid postID)
    {
        var post = await _db.Posts.FindAsync(postID);
        return post;
    }

    private async Task CompletePostObject(PostsModel post)
    {
        post.Restaurant = await _db.Restaurants.FindAsync(post.RestID);

        /* This is commented out because the below information are unused for now and adds a considerable loading time.
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

    private async Task<List<CommentsModel>> GetSpecificPostCommentsAsync(Guid postID)
    {
        var comments = await _db.Comments
            .OrderBy(c => c.CommentCreatedAt)
            .Where(c => c.PostID == postID)
            .ToListAsync();

        return comments;
    }

    private async Task<CommentsModel?> FetchCommentByIDAsync(Guid commentID)
    {
        var comment = await _db.Comments.FindAsync(commentID);
        return comment;
    }

}
