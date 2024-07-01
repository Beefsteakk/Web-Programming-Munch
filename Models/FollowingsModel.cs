using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class FollowingsModel
{
    [Key, Column(Order = 0, TypeName = "char(36)")]
    public Guid UserID { get; set; } // Composite Primary Key and Foreign Key From UsersModel

    [Key, Column(Order = 1, TypeName = "char(36)")]
    public Guid RestID { get; set; } // Composite Primary Key and Foreign Key From RestauarantsModel

    [Required]
    public DateTime FollowCreatedAt { get; set; }

    // Navigation properties
    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }

    [ForeignKey("RestID")]
    public required RestaurantsModel Rest { get; set; }

    public FollowingsModel()
    {
        FollowCreatedAt = DateTime.UtcNow;
    }
}
