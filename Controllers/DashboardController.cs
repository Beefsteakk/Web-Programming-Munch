using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace EffectiveWebProg.Controllers
{
    public class DashboardController : Controller
    {
        private readonly string connectionString = "server=mysql-webprogramming1-sit-cc31.c.aivencloud.com;port=19112;database=Munch;uid=avnadmin;pwd=AVNS_HsKVnqOod_xgB4OJwUT;sslmode=Required";

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
                                RestRatings = !string.IsNullOrEmpty(reader["RestRatings"].ToString()) ? int.Parse(reader["RestRatings"].ToString()) : 0
                            };
                        }
                    }
                }
            }
            return restaurantDetails;
        }

        private async Task<List<ReservationsModel>> GetUpcomingReservationsAsync()
        {
            List<ReservationsModel> reservations = new List<ReservationsModel>();
            string query = "SELECT * FROM Reservations WHERE ReservationDate >= CURDATE() ORDER BY ReservationDate, ReservationTime";
            
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
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

        private async Task<int> GetTotalReservationsCountAsync()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Reservations";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }
            return count;
        }

        public async Task<IActionResult> Index()
        {
            string userId = "deab13da-1e6b-11ef-ad56-662ef0370963"; // example userId
            RestaurantsModel restaurantDetails = await GetRestaurantDetailsByUserIdAsync(userId);
            List<ReservationsModel> upcomingReservations = await GetUpcomingReservationsAsync();
            int totalReservations = await GetTotalReservationsCountAsync();

            ViewBag.RestaurantDetails = restaurantDetails;
            ViewBag.UpcomingReservations = upcomingReservations;
            ViewBag.TotalReservations = totalReservations;

            return View();
        }
    }
}
