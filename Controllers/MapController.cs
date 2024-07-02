using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using EffectiveWebProg.Data;

namespace EffectiveWebProg.Controllers
{
    public class MapController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const string connectionString = "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";

        public MapController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public JsonResult GetLocations(string searchQuery = "", float? minRating = null, float? maxRating = null, Guid? categoryID = null)
        {
            List<object> locations = new List<object>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT r.RestID, r.RestName, r.RestLong, r.RestLat, r.RestRatings FROM Restaurants r " +
                                    "LEFT JOIN RestCategory rc ON r.RestID = rc.RestID " + 
                                    "LEFT JOIN Category c ON rc.CatID = c.CatID WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " AND r.RestName LIKE @searchQuery";
                    }

                    if (minRating.HasValue)
                    {
                        query += " AND r.RestRatings >= @minRating";
                    }

                    if (maxRating.HasValue)
                    {
                        query += " AND r.RestRatings <= @maxRating";
                    }

                    if (categoryID.HasValue)
                    {
                        query += " AND c.CatID = @categoryID";
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

                        if (categoryID.HasValue)
                        {
                            command.Parameters.AddWithValue("@categoryID", categoryID.Value);
                        }

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var location = new
                                {
                                    RestID = reader["RestID"].ToString(),
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
