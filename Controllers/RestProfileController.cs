using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace EffectiveWebProg.Controllers
{
    public class RestProfileController : Controller
    {
        private readonly string connectionString = "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";

        private async Task<string> GetRestaurantNameByUserIdAsync(string userId)
        {
            string restaurantName = null;
            string query = "SELECT RestName FROM Restaurants WHERE RestID = @UserId";

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
                            restaurantName = reader["RestName"].ToString();
                        }
                    }
                }
            }
            return restaurantName;
        }

        public async Task<IActionResult> Index()
        {
            string userId = "deab13da-1e6b-11ef-ad56-662ef0370963"; // example userId
            string restaurantName = await GetRestaurantNameByUserIdAsync(userId);

            ViewBag.RestaurantName = restaurantName;
            return View();
        }
    }
}