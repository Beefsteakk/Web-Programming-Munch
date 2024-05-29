using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class RestaurantsModel
{
    [Key]
    public Guid RestID { get; set; } // Primary Key

    public Guid? OwnerID { get; set; } // Foreign Key From UsersModel (Nullable)

    [StringLength(255)]
    public string? RestName { get; set; } // nullable

    public double? RestLat { get; set; } // nullable
    public double? RestLong { get; set; } // nullable

    public string? RestBio { get; set; } // nullable

    [StringLength(255)]
    public string? RestPic { get; set; } // nullable

    [StringLength(255)]
    public string? RestEmail { get; set; } // nullable

    [StringLength(255)]
    public string? RestWebsite { get; set; } // nullable
    

    // Navigation properties potentially linking to other relevant data
    [ForeignKey("OwnerID")]
    public UsersModel? Owner { get; set; }

    public ICollection<FoodListEntriesModel> FoodEntry { get; set; }
    public ICollection<ReservationsModel> Reservation { get; set; }
    public ICollection<RestaurantRatingsModel> RestaurantRating { get; set; }

    public RestaurantsModel()
    {
        RestID = Guid.NewGuid();
        FoodEntry = new List<FoodListEntriesModel>();
        Reservation = new List<ReservationsModel>();
        RestaurantRating = new List<RestaurantRatingsModel>();
    }

}
