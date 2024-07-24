using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace EffectiveWebProg.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly string connectionString = "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";

        
        public IActionResult Employees()
        {
            return RedirectToAction("Index", "Employees");
        }

        private async Task<RestaurantsModel> GetRestaurantDetailsByUserIdAsync(string userId)
        {
            RestaurantsModel restaurantDetails = null;
            string query = "SELECT * FROM Restaurants WHERE RestID = @UserId"; 

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
                                RestAddress = reader["RestAddress"].ToString(),
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

        private async Task<List<ReservationsModel>> GetUpcomingReservationsAsync(string restId)
        {
            List<ReservationsModel> reservations = new List<ReservationsModel>();
            string query = "SELECT * FROM Reservations WHERE ReservationDate >= CURDATE() AND RestID = @RestID ORDER BY ReservationDate, ReservationTime";
            
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restId);
                    using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ReservationsModel reservation = new ReservationsModel
                            {
                                ReservationID = Guid.Parse(reader["ReservationID"].ToString()),
                                RestID = Guid.Parse(reader["RestID"].ToString()),
                                UserID = Guid.Parse(reader["UserID"].ToString()),
                                ReservedName = reader["ReservedName"].ToString(),
                                ReservationDate = DateOnly.FromDateTime(DateTime.Parse(reader["ReservationDate"].ToString())),
                                ReservationTime = TimeSpan.Parse(reader["ReservationTime"].ToString()),
                                NumOfGuests = int.Parse(reader["NumOfGuests"].ToString()),
                                SpecialRequest = reader["SpecialRequest"].ToString(),
                                ReservationStatus = reader["ReservationStatus"].ToString() ?? "Pending",
                                // Initialize navigation properties with placeholder values
                                User = new UsersModel
                                {
                                    UserID = Guid.NewGuid(),
                                    UserName = "Placeholder",
                                    UserEmail = "placeholder@example.com",
                                    UserUsername = "PlaceholderUser",
                                    UserPassword = "PlaceholderPassword"
                                },
                                Restaurant = new RestaurantsModel
                                {
                                    RestID = Guid.NewGuid(),
                                    RestName = "PlaceholderRestaurant",
                                    RestBio = "PlaceholderBio",
                                    RestContact = 1234567890,
                                    RestEmail = "placeholder@restaurant.com",
                                    RestAddress = "123 Placeholder St",
                                    RestLat = 0.0,
                                    RestLong = 0.0,
                                    RestPic = null,
                                    RestWebsite = "http://placeholder.restaurant",
                                    RestRatings = 0
                                }
                            };
                            reservations.Add(reservation);
                        }
                    }
                }
            }
            return reservations;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservationStatus(Guid reservationId, string newStatus)
        {
            string query = "UPDATE Reservations SET ReservationStatus = @NewStatus WHERE ReservationID = @ReservationID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                    cmd.Parameters.AddWithValue("@ReservationID", reservationId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return Ok(new { success = true });
        }

        private async Task<int> GetTotalReservationsCountAsync(string restId)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Reservations WHERE RestID = @RestID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restId);
                    count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }
            return count;
        }

        private async Task<int> GetTotalEmployeesCountAsync(string restId)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Employees WHERE RestID = @RestID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restId);
                    count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }
            return count;
        }

        private async Task<int> GetTotalPostCountAsync(string restId)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Posts WHERE RestID = @RestID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restId);
                    count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }
            return count;
        }

        public async Task<IActionResult> Index()
        {
            string restID = HttpContext.Session.GetString("SSID") ?? "";
            if (string.IsNullOrEmpty(restID))
            {
                Console.WriteLine("RestID is null or empty: " + restID);
                // Handle the case where the restID is not available in the session
                return RedirectToAction("Error", "Home");
            }
            //string restId = "deab13da-1e6b-11ef-ad56-662ef0370963"; // example restaurant ID
            RestaurantsModel restaurantDetails = await GetRestaurantDetailsByUserIdAsync(restID);
            List<ReservationsModel> upcomingReservations = await GetUpcomingReservationsAsync(restID);
            int totalReservations = await GetTotalReservationsCountAsync(restID);
            int totalEmployees = await GetTotalEmployeesCountAsync(restID);
            int totalPosts = await GetTotalPostCountAsync(restID);
            var reservationStats = await GetReservationStatsAsync(restID);
            var itemStocks = await GetItemStocksAsync(restID);
            var employeeWorkingHours = await GetEmployeeWorkingHoursAsync(restID);

            ViewBag.RestaurantDetails = restaurantDetails;
            ViewBag.UpcomingReservations = upcomingReservations;
            ViewBag.TotalReservations = totalReservations;
            ViewBag.TotalEmployees = totalEmployees;
            ViewBag.ReservationStats = reservationStats;
            ViewBag.ItemStocks = itemStocks;
            ViewBag.EmployeeWorkingHours = employeeWorkingHours;
            ViewBag.TotalPosts = totalPosts;

            return View();
        }

        private async Task<Dictionary<string, int>> GetReservationStatsAsync(string restId)
        {
            var stats = new Dictionary<string, int>();
            string query = @"
                SELECT ReservationStatus, COUNT(*) as Count
                FROM Reservations
                WHERE ReservationDate >= DATE_SUB(CURDATE(), INTERVAL 30 DAY) AND RestID = @RestID
                GROUP BY ReservationStatus";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restId);
                    using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            stats[reader["ReservationStatus"].ToString()] = int.Parse(reader["Count"].ToString());
                        }
                    }
                }
            }
            return stats;
        }

        private async Task<Dictionary<string, int>> GetItemStocksAsync(string restID)
        {
            var stocks = new Dictionary<string, int>();
            string query = "SELECT Items.ItemName, InventoryItems.StockCount " +
                        "FROM InventoryItems " +
                        "JOIN Items ON InventoryItems.ItemID = Items.ItemID " +
                        "JOIN Inventory ON Inventory.InventoryID = InventoryItems.InventoryID " +
                        "WHERE Inventory.RestID = @restID";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Add the restID parameter to the command
                    cmd.Parameters.AddWithValue("@restID", restID);

                    using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            stocks[reader["ItemName"].ToString()] = int.Parse(reader["StockCount"].ToString());
                        }
                    }
                }
            }
            return stocks;
        }


        private async Task<Dictionary<string, double>> GetEmployeeWorkingHoursAsync(string restId)
        {
            var workingHours = new Dictionary<string, double>();
            string query = @"
                SELECT E.EmployeeName, SUM(TIMESTAMPDIFF(HOUR, T.StartTime, T.EndTime)) AS TotalHours
                FROM TimeSheet T
                JOIN Employees E ON T.EmployeeID = E.EmployeeID
                WHERE E.RestID = @RestID
                GROUP BY E.EmployeeName";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RestID", restId);
                    using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            workingHours[reader["EmployeeName"].ToString()] = double.Parse(reader["TotalHours"].ToString());
                        }
                    }
                }
            }

            return workingHours;
        }
    }
}
