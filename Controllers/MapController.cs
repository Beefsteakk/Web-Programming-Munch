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
        public JsonResult GetLocations()
        {
            List<object> locations = new List<object>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT RestName, RestLong, RestLat FROM Restaurants";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var location = new
                                {
                                    Name = reader["RestName"].ToString(),
                                    Lat = Convert.ToDouble(reader["RestLat"]),
                                    Lng = Convert.ToDouble(reader["RestLong"])
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


        //[HttpGet]
        // select the restaurant on the map and it will direct to the restaurant profile
    }
}