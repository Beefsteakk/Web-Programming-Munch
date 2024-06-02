using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class UserFollowingsModel
{
    [Key, Column(Order = 0)]
    public Guid FollowerID { get; set; } // Composite Primary Key and Foreign Key From UsersModel

    [Key, Column(Order = 1)]
    public Guid FollowedUserID { get; set; } // Composite Primary Key and Foreign Key From UsersModel

    [Required]
    public DateTime FollowCreatedAt { get; set; }




    // Navigation properties
    [ForeignKey("FollowerID")]
    public required UsersModel Follower { get; set; }

    [ForeignKey("FollowedUserID")]
    public required UsersModel FollowedUser { get; set; }



    public UserFollowingsModel()
    {
        FollowCreatedAt = DateTime.UtcNow;
    }
}
