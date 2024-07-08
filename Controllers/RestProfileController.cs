using EffectiveWebProg.Models;
using EffectiveWebProg.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EffectiveWebProg.Controllers
{
    public class RestProfileController(ApplicationDbContext db) : BaseController
    {
        private readonly string connectionString = "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";
        private readonly ApplicationDbContext _db = db;

        private async Task<RestaurantsModel?> GetRestaurantDetailsByUserIdAsync(string restID)
        {
            var restaurant = await _db.Restaurants.FindAsync(Guid.Parse(restID));
            return restaurant;
        }


        private async Task<List<PostPicsModel>> GetRestaurantPostsAsync(string restID)
        {
            List<PostPicsModel> postDetailsList = new List<PostPicsModel>();
            string query = "SELECT pp.PicID, pp.ImageURL FROM PostPics pp JOIN Posts p on pp.PostID = p.PostID WHERE p.RestID = @RestID ORDER BY p.PostCreatedAt DESC";

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
                                PicID = Guid.Parse(reader["PicID"].ToString() ?? ""),
                                ImageURL = reader["ImageURL"].ToString() ?? "",
                            };
                            postDetailsList.Add(postDetails);
                        }
                    }
                }
            }

            foreach (var post in postDetailsList)
            {
                Console.WriteLine("PostDetails lol: " + post.ImageURL);
            }

            return postDetailsList;
        }

        private async Task<Dictionary<string, int>> GetFollowerCountsAsync()
        {
            var followerCounts = new Dictionary<string, int>();
            string query = "SELECT FollowedRestID, COUNT(UserID) AS FollowerCount FROM Munch.RestaurantFollowings GROUP BY FollowedRestID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string restaurantId = reader["FollowedRestID"].ToString();
                            int followerCount = int.Parse(reader["FollowerCount"].ToString());
                            followerCounts[restaurantId] = followerCount;
                        }
                    }
                }
            }
            return followerCounts;
        }

        public async Task<IActionResult> Index()
        {
            string restID = HttpContext.Session.GetString("RestID") ?? "";
            if (string.IsNullOrEmpty(restID))
            {
                Console.WriteLine("RestID is null or empty: " + restID);
                // Handle the case where the restID is not available in the session
                return RedirectToAction("Error", "Home");
            }

            RestaurantsModel restaurantDetails = await GetRestaurantDetailsByUserIdAsync(restID);
            List<PostPicsModel> restaurantPosts = await GetRestaurantPostsAsync(restID);
            string sessionemail = HttpContext.Session.GetString("SSName") ?? "";

            string sessionType = HttpContext.Session.GetString("SSUserType") ?? "";
            bool isOwnRestaurant = sessionemail == restaurantDetails.RestEmail;

            ViewBag.SessionEmail = sessionemail;
            ViewBag.RestaurantDetails = restaurantDetails;

            var followerCounts = await GetFollowerCountsAsync();
            ViewBag.FollowerCounts = followerCounts;
            
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
        public async Task<IActionResult> SaveProfile(RestaurantsModel restaurantDetails)
        {
            string query = "UPDATE Restaurants SET RestName = @RestName, RestBio = @RestBio, RestContact = @RestContact, RestEmail = @RestEmail, RestAddress = @RestAddress, RestLat = @RestLat, RestLong = @RestLong, RestPic = @RestPic, RestWebsite = @RestWebsite, RestRatings = @RestRatings WHERE RestID = @RestID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restaurantDetails.RestID);
                    cmd.Parameters.AddWithValue("@RestName", restaurantDetails.RestName);
                    cmd.Parameters.AddWithValue("@RestBio", restaurantDetails.RestBio);
                    cmd.Parameters.AddWithValue("@RestContact", restaurantDetails.RestContact);
                    cmd.Parameters.AddWithValue("@RestEmail", restaurantDetails.RestEmail);
                    cmd.Parameters.AddWithValue("@RestAddress", restaurantDetails.RestAddress);
                    cmd.Parameters.AddWithValue("@RestLat", restaurantDetails.RestLat);
                    cmd.Parameters.AddWithValue("@RestLong", restaurantDetails.RestLong);
                    cmd.Parameters.AddWithValue("@RestPic", restaurantDetails.RestPic);
                    cmd.Parameters.AddWithValue("@RestWebsite", restaurantDetails.RestWebsite);
                    cmd.Parameters.AddWithValue("@RestRatings", restaurantDetails.RestRatings);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost("SaveReservation")]
        [Route("RestProfile/SaveReservation")]
        public async Task<IActionResult> SaveReservation(ReservationsModel reservationDetails, string paymentToken)
        {
            // Retrieve the UserID from the claims
            string userID = "0df6efb3-87e7-411d-adba-d9e26cacc017";
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

    
    }

}
