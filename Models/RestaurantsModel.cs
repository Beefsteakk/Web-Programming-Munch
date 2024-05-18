using System;
using System.ComponentModel.DataAnnotations;

namespace EffectiveWebProg.Models;

public class RestaurantsModel
{
    [Key]
    public Guid RestID { get; set; } // Primary Key

    [Required]
    public Guid OwnerID { get; set; } // Foreign Key From UsersEntity

    [StringLength(255)]
    public string RestName { get; set; }

    public double RestLat { get; set; }
    public double RestLong { get; set; }

    public string RestBio { get; set; }

    [StringLength(255)]
    public string RestPic { get; set; }

    [StringLength(255)]
    public string RestEmail { get; set; }

    [StringLength(255)]
    public string RestWebsite { get; set; }
    

    public RestaurantsModel()
    {
        RestID = Guid.NewGuid();
    }

    // Navigation properties potentially linking to other relevant data
    public UsersModel Owner { get; set; }

    public ICollection<FoodListEntriesModel> FoodEntry { get; set; }
    public ICollection<ReservationModel> Reservation { get; set; }
}
