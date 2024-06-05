using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class PostLikesRestModel
{
    [Key, Column(Order = 0)]
    public Guid PostID { get; set; } // Composite Primary Key and Foreign Key From PostsModel

    [Key, Column(Order = 1)]
    public Guid RestID { get; set; } // Composite Primary Key and Foreign Key From RestaurantsModel

    public DateTime LikeCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("PostID")]
    public required PostsModel Post { get; set; }

    [ForeignKey("RestID")]
    public required RestaurantsModel Restaurant { get; set; }


    public PostLikesRestModel()
    {
        LikeCreatedAt = DateTime.UtcNow;
    }
}
