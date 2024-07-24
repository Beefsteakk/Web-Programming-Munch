using EffectiveWebProg.Models;
using EffectiveWebProg.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EffectiveWebProg.Controllers
{
    public class RestProfileController(ApplicationDbContext db) : BaseController
    {
        private readonly string connectionString = "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";
        private readonly ApplicationDbContext _db = db;

        private async Task<RestaurantsModel?> GetRestaurantDetailsByUserIdAsync(string restID)
        {
            RestaurantsModel? restaurantDetails = null;
            string query = "SELECT RestID, RestName, RestLat, RestLong, RestAddress, RestEmail, RestPassword, RestContact, RestBio, RestPic, RestWebsite, RestOpenHr, RestCloseHr, RestCoverPic FROM Restaurants WHERE RestID = @RestID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restID);

                    using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            restaurantDetails = new RestaurantsModel
                            {
                                RestID = Guid.Parse(reader["RestID"].ToString() ?? ""),
                                RestName = reader["RestName"].ToString() ?? "",
                                RestLat = reader.IsDBNull(reader.GetOrdinal("RestLat")) ? (double?)null : reader.GetDouble("RestLat"),
                                RestLong = reader.IsDBNull(reader.GetOrdinal("RestLong")) ? (double?)null : reader.GetDouble("RestLong"),
                                RestAddress = reader["RestAddress"].ToString() ?? "",
                                RestEmail = reader["RestEmail"].ToString() ?? "",
                                RestPassword = reader["RestPassword"].ToString() ?? "",
                                RestContact = reader.IsDBNull(reader.GetOrdinal("RestContact")) ? (int?)null : reader.GetInt32("RestContact"),
                                RestBio = reader["RestBio"].ToString() ?? "",
                                RestPic = reader["RestPic"].ToString() ?? "",
                                RestWebsite = reader["RestWebsite"].ToString() ?? "",
                                RestOpenHr = reader.IsDBNull(reader.GetOrdinal("RestOpenHr")) ? (TimeSpan?)null : reader.GetTimeSpan("RestOpenHr"),
                                RestCloseHr = reader.IsDBNull(reader.GetOrdinal("RestCloseHr")) ? (TimeSpan?)null : reader.GetTimeSpan("RestCloseHr"),
                                RestCoverPic = reader["RestCoverPic"].ToString() ?? "",
                            };
                        }
                    }
                }
            }

            return restaurantDetails;
        }


        private async Task<List<PostPicsModel>> GetRestaurantPostsAsync(string restID)
        {
            List<PostPicsModel> postDetailsList = new List<PostPicsModel>();
            string query = "SELECT pp.PostID, pp.PicID, pp.ImageURL FROM PostPics pp JOIN Posts p on pp.PostID = p.PostID WHERE p.RestID = @RestID ORDER BY p.PostCreatedAt DESC";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restID);

                    using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            PostPicsModel postDetails = new PostPicsModel
                            {
                                PostID = Guid.Parse(reader["PostID"].ToString() ?? ""),
                                PicID = Guid.Parse(reader["PicID"].ToString() ?? ""),
                                ImageURL = reader["ImageURL"].ToString() ?? "",
                            };
                            postDetailsList.Add(postDetails);
                        }
                    }
                }
            }



            return postDetailsList;
        }
        public async Task<IActionResult> SelectRestaurant(Guid restID)
        {
            // Set the RestID in the session
            HttpContext.Session.SetString("RestID", restID.ToString());

            // Redirect to the employee index page or wherever appropriate
            return RedirectToAction("Index");
        }


        private async Task<int> GetRestaurantFollowersCount(string restID)
        {
            int followersCount = 0;
            string query = "SELECT COUNT(*) FROM Followings WHERE RestID = @RestID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restID);

                    followersCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }

            return followersCount;
        }

        private async Task<bool> IsFollowing(string restID, string userID)
        {
            string query = "SELECT EXISTS(SELECT 1 FROM Followings WHERE RestID = @RestID AND UserID = @UserID)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restID);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToBoolean(result);
                }
            }
        }

        public async Task<IActionResult> Index()
        {
            string restID = HttpContext.Session.GetString("RestID") ?? "";
            string userID = HttpContext.Session.GetString("SSID") ?? "";
            if (string.IsNullOrEmpty(restID))
            {
                Console.WriteLine("RestID is null or empty: " + restID);
                // Handle the case where the restID is not available in the session
                return RedirectToAction("Error", "Home");
            }
            if (!string.IsNullOrEmpty(userID))
            {
                ViewBag.isFollowing = await IsFollowing(restID, userID);
            }

            RestaurantsModel restaurantDetails = await GetRestaurantDetailsByUserIdAsync(restID);
            List<PostPicsModel> restaurantPosts = await GetRestaurantPostsAsync(restID);

            int count = restaurantPosts.Count;
            ViewBag.PostCount = count;


            string sessionemail = HttpContext.Session.GetString("SSName") ?? "";

            string sessionType = HttpContext.Session.GetString("SSUserType") ?? "";
            bool isOwnRestaurant = sessionemail == restaurantDetails.RestEmail;

            ViewBag.SessionEmail = sessionemail;
            ViewBag.RestaurantDetails = restaurantDetails;
            ViewBag.isOwnRestaurant = isOwnRestaurant;
            ViewBag.RestaurantPosts = restaurantPosts;
            ViewBag.FollowersCount = await GetRestaurantFollowersCount(restID);

            return View();
        }

        public async Task<IActionResult> EditProfile()
        {
            string sessionID = HttpContext.Session.GetString("SSName") ?? "";
            string restID = HttpContext.Session.GetString("RestID") ?? "";

            RestaurantsModel restaurantDetails = await GetRestaurantDetailsByUserIdAsync(restID);
            ViewBag.RestaurantDetails = restaurantDetails;


            return View();
        }

        [HttpPost("SaveProfile")]
        [Route("RestProfile/SaveProfile")]
        public async Task<IActionResult> SaveProfile(RestaurantsModel restaurantDetails, IFormFile? RestPic, IFormFile? RestCoverPic)
        {
            Console.WriteLine(restaurantDetails.RestName);
            var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
            var restaurant = await _db.Restaurants.FirstAsync(r => r.RestID == sessionID);
            if (restaurant == null)
            {
                Response.StatusCode = 500;
                return Content("Internal Server Error: An unknown issue was encountered, please retry.");
            }

            if (restaurantDetails == null)
            {
                Console.WriteLine("restaurantDetails is null");
                return BadRequest("Restaurant details are required.");
            }

            if (RestPic != null)
            {
                Console.WriteLine("Restpic id " + RestPic);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "RestProfilePics");
                string filePath;
                var filename = $"{restaurantDetails.RestID}{Path.GetExtension(RestPic.FileName)}";
                filePath = Path.Combine(uploadsFolder, filename);

                // Check if the file already exists
                if (System.IO.File.Exists(filePath))
                {
                    // Handle the case where the file already exists
                    // For example, you can delete the existing file, rename it, or log a message
                    Console.WriteLine("File already exists. Deleting the existing file.");
                    System.IO.File.Delete(filePath); // This will delete the existing file
                }

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await RestPic.CopyToAsync(fileStream);
                restaurant.RestPic = filename;
            }
            else
            {
                Console.WriteLine("RestPic is null");
            }

            if (RestCoverPic != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "RestCoverPics");
                string filePath;
                if (!string.IsNullOrEmpty(restaurantDetails.RestCoverPic))
                {
                    filePath = Path.Combine(uploadsFolder, restaurantDetails.RestCoverPic);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                var filename = $"{restaurantDetails.RestID}{Path.GetExtension(RestCoverPic.FileName)}";
                filePath = Path.Combine(uploadsFolder, filename);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await RestCoverPic.CopyToAsync(fileStream);
                restaurant.RestCoverPic = filename;
            }
            else
            {
                Console.WriteLine("RestCoverPic is null");
            }

            restaurant.RestName = restaurantDetails.RestName;
            restaurant.RestEmail = restaurantDetails.RestEmail;
            restaurant.RestContact = restaurantDetails.RestContact;
            restaurant.RestWebsite = restaurantDetails.RestWebsite;
            restaurant.RestBio = restaurantDetails.RestBio;
            restaurant.RestOpenHr = restaurantDetails.RestOpenHr;
            restaurant.RestCloseHr = restaurantDetails.RestCloseHr;
            restaurant.RestLat = restaurantDetails.RestLat;
            restaurant.RestLong = restaurantDetails.RestLong;
            _db.Restaurants.Update(restaurant);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost("SaveReservation")]
        [Route("RestProfile/SaveReservation")]
        public async Task<IActionResult> SaveReservation(ReservationsModel reservationDetails, string paymentToken)
        {
            // Retrieve the UserID from the claims
            string userID = HttpContext.Session.GetString("SSID") ?? "";
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest("UserID is not available.");
            }

            // Check if UserID exists in the Users table
            string userCheckQuery = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID";
            bool userExists = false;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand userCmd = new MySqlCommand(userCheckQuery, conn))
                {
                    userCmd.Parameters.AddWithValue("@UserID", userID);
                    userExists = (long)await userCmd.ExecuteScalarAsync() > 0;
                }

                if (!userExists)
                {
                    return BadRequest("Invalid UserID.");
                }

                string query = "INSERT INTO Reservations (RestID, ReservedName, ReservationDate, NumOfGuests, SpecialRequest, ReservationStatus, ReservationTime, paymentToken, UserID) VALUES (@RestID, @ReservedName, @ReservationDate, @NumOfGuests, @SpecialRequest, @ReservationStatus, @ReservationTime, @paymentToken, @UserID)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", reservationDetails.RestID);
                    cmd.Parameters.AddWithValue("@ReservedName", reservationDetails.ReservedName);
                    cmd.Parameters.AddWithValue("@ReservationDate", reservationDetails.ReservationDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@NumOfGuests", reservationDetails.NumOfGuests);
                    cmd.Parameters.AddWithValue("@SpecialRequest", reservationDetails.SpecialRequest);
                    cmd.Parameters.AddWithValue("@ReservationStatus", "Pending");
                    cmd.Parameters.AddWithValue("@ReservationTime", reservationDetails.ReservationTime);
                    cmd.Parameters.AddWithValue("@paymentToken", paymentToken);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Restaurant/Follow")]
        public async Task<JsonResult> FollowAsync(Guid restaurantId)
        {
            var restaurant = await _db.Restaurants.FindAsync(restaurantId);
            if (restaurant == null) return Json(new {status = "failed", reason = "Restaurant not found."});

            var sessionID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
            var user = await _db.Users.FindAsync(sessionID);
            if (user == null) return Json(new {status = "failed", reason = "User not found."});

            var follow = await _db.Followings.FirstOrDefaultAsync(f => f.UserID == user.UserID && f.RestID == restaurant.RestID);
            var type = "";
            if (follow == null)
            {
                follow = new FollowingsModel { Rest = restaurant, User = user };
                await _db.Followings.AddAsync(follow);
                type = "follow";
            }
            else
            {
                _db.Followings.Remove(follow);
                type = "unfollow";
            }

            await _db.SaveChangesAsync();
            return Json(new {status = "success", type = type});
        }
    }
}
