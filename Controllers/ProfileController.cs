using EffectiveWebProg.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EffectiveWebProg.ViewModels;
using EffectiveWebProg.Models;
using MySql.Data.MySqlClient;

namespace EffectiveWebProg.Controllers
{

    public class ProfileController : BaseController
    {
        private readonly ApplicationDbContext _db;
        private readonly string connectionString = "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";

        public ProfileController(ApplicationDbContext db)
        {
            _db = db;
        }

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


        [HttpPost]
        public async Task<IActionResult> UpdateReservation(Guid reservationId, string reservedName, DateOnly reservationDate, string reservationTime, int NumberOfGuest, string SpecialRequest)
        {
            string query = "UPDATE Reservations SET ReservedName=@ReservedName, ReservationDate=@ReservationDate, ReservationTime=@ReservationTime, NumOfGuests=@NumOfGuests, SpecialRequest=@SpecialRequest WHERE ReservationID = @ReservationID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                    cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                    cmd.Parameters.AddWithValue("@ReservedName", reservedName);
                    cmd.Parameters.AddWithValue("@ReservationDate", reservationDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@ReservationTime", reservationTime);
                    cmd.Parameters.AddWithValue("@NumOfGuests", NumberOfGuest);
                    cmd.Parameters.AddWithValue("@SpecialRequest", SpecialRequest);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return Ok(new { success = true });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            Console.WriteLine("Deleting reservation with ID: " + id);
            string query = "DELETE FROM Reservations WHERE ReservationID = @ReservationID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReservationID", id);

                    var result = await cmd.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return Ok(new { success = true });
                    }
                    else
                    {
                        return NotFound(new { success = false, message = "Reservation not found." });
                    }
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
            var user = await _db.Users.FirstAsync(u => u.UserID == sessionID);
            return View(user);
        }

        [HttpPost("EditProfile")]
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
