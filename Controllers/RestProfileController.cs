using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.ComponentModel;
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
                                RestName = reader["RestName"].ToString(),
                                RestBio = reader["RestBio"].ToString(),
                                RestContact = int.Parse(reader["RestContact"].ToString()),
                                RestEmail = reader["RestEmail"].ToString(),
                                RestAddress = reader["RestAddress"].ToString(),
                                RestLat = double.Parse(reader["RestLat"].ToString()),
                                RestLong=double.Parse(reader["RestLong"].ToString()),
                                RestPic=reader["RestPic"].ToString(),
                                RestWebsite=reader["RestWebsite"].ToString(),
                                RestRatings=int.Parse(reader["RestRatings"].ToString())


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
    }
}
