using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace EffectiveWebProg.Controllers
{
    public class MapController : Controller
    {
        private const string connectionString = "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";

        [HttpGet]
        public JsonResult GetLocations(string searchQuery = "", float? minRating = null, float? maxRating = null)
        {
            List<object> locations = new List<object>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT RestName, RestLong, RestLat, RestRatings FROM Restaurants WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " AND RestName LIKE @searchQuery";
                    }

                    if (minRating.HasValue)
                    {
                        query += " AND RestRatings >= @minRating";
                    }

                    if (maxRating.HasValue)
                    {
                        query += " AND RestRatings <= @maxRating";
                    }

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            command.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
                        }

                        if (minRating.HasValue)
                        {
                            command.Parameters.AddWithValue("@minRating", minRating.Value);
                        }

                        if (maxRating.HasValue)
                        {
                            command.Parameters.AddWithValue("@maxRating", maxRating.Value);
                        }

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var location = new
                                {
                                    Name = reader["RestName"].ToString(),
                                    Lat = reader["RestLat"] != DBNull.Value ? Convert.ToDouble(reader["RestLat"]) : (double?)null,
                                    Lng = reader["RestLong"] != DBNull.Value ? Convert.ToDouble(reader["RestLong"]) : (double?)null,
                                    Rating = reader["RestRatings"] != DBNull.Value ? Convert.ToSingle(reader["RestRatings"]) : (float?)null
                                };
                                locations.Add(location);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }

            return Json(locations);
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["meow"] = "Restaurants On Munch";
            return View();
        }
    }
}
