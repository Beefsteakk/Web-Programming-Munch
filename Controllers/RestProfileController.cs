using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EffectiveWebProg.Controllers
{
    public class RestProfileController : BaseController
    {
        private readonly string connectionString = "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";

        private async Task<RestaurantsModel> GetRestaurantDetailsByUserIdAsync(string restID)
        {
            RestaurantsModel restaurantDetails = null;
            string query = "SELECT * FROM Restaurants WHERE RestID = @RestID";

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
                                RestRatings = !string.IsNullOrEmpty(reader["RestRatings"].ToString()) ? float.Parse(reader["RestRatings"].ToString()) : 0
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
        public async Task<IActionResult> SelectRestaurant(string restID)
        {
            // Store the restID in session
            HttpContext.Session.SetString("RestID", restID);

            // Redirect to the restaurant profile or any other page
            return RedirectToAction("Index");
        }

        [HttpGet]
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
            ViewBag.IsOwnRestaurant = isOwnRestaurant;
            ViewBag.RestaurantPosts = restaurantPosts;

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
    }
}
