using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class RestaurantFollowingsModel
{
    [Key, Column(Order = 0)]
    public Guid FollowerID { get; set; } // Composite Primary Key and Foreign Key From UsersModel

    [Key, Column(Order = 1)]
    public Guid FollowedRestID { get; set; } // Composite Primary Key and Foreign Key From RestaurantsModel

    [Required]
    public DateTime FollowCreatedAt { get; set; }




    // Navigation properties
    [ForeignKey("FollowerID")]
    public required UsersModel Follower { get; set; }

    [ForeignKey("FollowedRestID")]
    public required RestaurantsModel FollowedRest { get; set; }



    public RestaurantFollowingsModel()
    {
        FollowCreatedAt = DateTime.UtcNow;
    }
}
