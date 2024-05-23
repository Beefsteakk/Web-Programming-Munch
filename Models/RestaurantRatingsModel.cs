using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EffectiveWebProg.Models;

public class RestaurantRatingsModel
{
    [Key]
    public Guid RatingID { get; set; } // Primary Key
    
    [Required]
    public Guid UserID { get; set; } // Foreign Key From UsersModel

    [Required]
    public Guid RestID { get; set; } // Foreign Key From RestaurantModel

    [Required]
    public int RatingValue { get; set; }
    public DateTime RatingCreatedAt { get; set; }



    public RestaurantRatingsModel()
    {
        RatingID = Guid.NewGuid();
    }

    // Navigation properties
    public UsersModel User { get; set; }
    public RestaurantsModel Restaurant { get; set; }
}
