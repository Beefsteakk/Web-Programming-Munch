using EffectiveWebProg.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EffectiveWebProg.ViewModels;

namespace EffectiveWebProg.Controllers
{
    public class ProfileController(ApplicationDbContext db) : BaseController
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("SSUserType") != "User") return Forbid();
            var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
            var user = await _db.Users.FirstAsync(u => u.UserID == sessionID);
            var followingCount = await _db.Followings.Where(f => f.UserID == user.UserID).ToListAsync();
            if (user == null) {
                Response.StatusCode = 500;
                return Content("Internal Server Error: An unknown issue was encountered, please retry.");
            }
            var viewModel = new ProfileViewModel(user, followingCount.Count);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
            var user = await _db.Users.FirstAsync(u => u.UserID == sessionID);
            return View(user);
        }

        [HttpPost("Profile/EditProfile")]
        public async Task<IActionResult> EditProfilePost(IFormFile UserProfilePic, string UserName, string UserUsername, string UserEmail, int UserContactNum)
        {
            var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
            var user = await _db.Users.FirstAsync(u => u.UserID == sessionID);
            if (user == null) {
                Response.StatusCode = 500;
                return Content("Internal Server Error: An unknown issue was encountered, please retry.");
            }

            var existingEmails = await _db.Users.Where(u => string.Equals(u.UserEmail.ToLower(), UserEmail.ToLower())).ToListAsync();
            if (!string.Equals(user.UserEmail.ToLower(), UserEmail.ToLower()) && existingEmails.Count > 0)
            {
                // TODO: Display an error when the email the user is trying to change to is already used by another user.
                return RedirectToAction("EditProfile");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "UserProfilePics");
            string filePath;
            if (user.UserProfilePic != null)
            {
                filePath = Path.Combine(uploadsFolder, user.UserProfilePic);
                System.IO.File.Delete(filePath);
            }

            var filename = $"{user.UserID}{Path.GetExtension(UserProfilePic.FileName)}";
            filePath = Path.Combine(uploadsFolder, filename);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await UserProfilePic.CopyToAsync(fileStream);

            user.UserProfilePic = filename;
            user.UserName = UserName;
            user.UserUsername = UserUsername;
            user.UserEmail = UserEmail;
            user.UserContactNum = UserContactNum;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("");
        }
    }
}
