using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class PostLikesUserModel
{
    [Key, Column(Order = 0)]
    public Guid PostID { get; set; } // Composite Primary Key and Foreign Key From PostsModel

    [Key, Column(Order = 1)]
    public Guid UserID { get; set; } // Composite Primary Key and Foreign Key From UsersModel

    public DateTime LikeCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("PostID")]
    public required PostsModel Post { get; set; }

    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }


    public PostLikesUserModel()
    {
        LikeCreatedAt = DateTime.UtcNow;
    }
}
