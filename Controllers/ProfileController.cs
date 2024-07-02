using EffectiveWebProg.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (user == null) {
                Response.StatusCode = 500;
                return Content("Internal Server Error: An unknown issue was encountered, please retry.");
            }
            return View(user);
        }
    }
}