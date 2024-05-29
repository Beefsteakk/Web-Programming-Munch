using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class RestaurantRatingsModel
{
    [Key]
    public Guid RatingID { get; set; } // Primary Key
    
    [Required]
    public Guid UserID { get; set; } // Foreign Key From UsersModel

    [Required]
    public Guid RestID { get; set; } // Foreign Key From RestaurantsModel

    [Required]
    public int RatingValue { get; set; }
    public DateTime RatingCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }

    [ForeignKey("RestID")]
    public required RestaurantsModel Restaurant { get; set; }


    public RestaurantRatingsModel()
    {
        RatingID = Guid.NewGuid();
        RatingCreatedAt = DateTime.UtcNow;
    }
}
