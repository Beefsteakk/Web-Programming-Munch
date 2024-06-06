using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EffectiveWebProg.Controllers
{
    public class RestProfileController : Controller
    {
        private readonly string connectionString =  "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";

        private async Task<RestaurantsModel> GetRestaurantDetailsByUserIdAsync(string userId)
        {
            RestaurantsModel restaurantDetails = null;
            string query = "SELECT * FROM Restaurants WHERE RestID = @UserId"; // Updated query

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            restaurantDetails = new RestaurantsModel
                            {
                                RestID = Guid.Parse(reader["RestID"].ToString()),
                                RestName = reader["RestName"].ToString(),
                                RestBio = reader["RestBio"]?.ToString(),
                                RestContact = !string.IsNullOrEmpty(reader["RestContact"].ToString()) ? int.Parse(reader["RestContact"].ToString()) : 0,
                                RestEmail = reader["RestEmail"].ToString(),
                                RestAddress = reader["RestAddress"]?.ToString(),
                                RestLat = !string.IsNullOrEmpty(reader["RestLat"].ToString()) ? double.Parse(reader["RestLat"].ToString()) : 0.0,
                                RestLong = !string.IsNullOrEmpty(reader["RestLong"].ToString()) ? double.Parse(reader["RestLong"].ToString()) : 0.0,
                                RestPic = reader["RestPic"]?.ToString(),
                                RestWebsite = reader["RestWebsite"]?.ToString(),
                                RestRatings = !string.IsNullOrEmpty(reader["RestRatings"].ToString()) ? int.Parse(reader["RestRatings"].ToString()) : 0
                            };
                        }
                    }
                }
            }
            return restaurantDetails;
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
            string userId = "deab13da-1e6b-11ef-ad56-662ef0370963"; // example userId
            RestaurantsModel restaurantDetails = await GetRestaurantDetailsByUserIdAsync(userId);

            ViewBag.RestaurantDetails = restaurantDetails;

            var followerCounts = await GetFollowerCountsAsync();
            ViewBag.FollowerCounts = followerCounts;
            
            return View();
        }

        public async Task<IActionResult> EditProfile()
        {
            string userId = "deab13da-1e6b-11ef-ad56-662ef0370963"; // example userId
            RestaurantsModel restaurantDetails = await GetRestaurantDetailsByUserIdAsync(userId);

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
    }
}
