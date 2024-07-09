using EffectiveWebProg.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EffectiveWebProg.ViewModels;
using EffectiveWebProg.Models;

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
            var ReservationsDetails = await GetReservationsByUserIdAsync(sessionID);
            ViewBag.ReservationsDetails = ReservationsDetails;

            if (user == null)
            {
                Response.StatusCode = 500;
                return Content("Internal Server Error: An unknown issue was encountered, please retry.");
            }
            var viewModel = new ProfileViewModel(user, followingCount.Count);
            return View(viewModel);
        }
        private async Task<List<ReservationsModel>> GetReservationsByUserIdAsync(Guid userId)
        {
            Console.WriteLine("sessionlol: " + userId);

            try
            {
                // Fetch all reservations asynchronously based on UserID
                var reservations = await _db.Reservations
                                            .Where(r => r.UserID == userId)
                                            .ToListAsync();

                // Log each reservation
                if (reservations != null && reservations.Count > 0)
                {
                    Console.WriteLine($"Found {reservations.Count} reservations for UserID: " + userId);
                    foreach (var reservation in reservations)
                    {
                        Console.WriteLine("Reservation found:");
                        Console.WriteLine("Reservation ID: " + reservation.ReservationID);
                        Console.WriteLine("User ID: " + reservation.UserID);
                        Console.WriteLine("Restaurant ID: " + reservation.RestID);
                        Console.WriteLine("Number of Guests: " + reservation.NumOfGuests);
                        Console.WriteLine("Special Request: " + reservation.SpecialRequest);
                        Console.WriteLine("Reservation Status: " + reservation.ReservationStatus);
                        Console.WriteLine("Date: " + reservation.ReservationDate);
                        Console.WriteLine("Time: " + reservation.ReservationTime);
                        Console.WriteLine("Reserved Name: " + reservation.ReservedName);
                    }
                }
                else
                {
                    Console.WriteLine("No reservations found for UserID: " + userId);
                }

                return reservations;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return new List<ReservationsModel>(); // Return an empty list in case of error
            }
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
            if (user == null)
            {
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
