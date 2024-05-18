using System;
using System.ComponentModel.DataAnnotations;

namespace EffectiveWebProg.Models;

public class PostLikesModel
{
    [Key]
    public Guid PostLikeID { get; set; } // Primary Key

    [Required]
    public Guid PostID { get; set; } // Foreign Key From PostsEntity

    [Required]
    public Guid UserID { get; set; } // Foreign Key From UsersEntity

    public DateTime LikeCreatedAt { get; set; }


    public PostLikesModel()
    {
        PostLikeID = Guid.NewGuid();
    }

    // Navigation properties
    public PostsModel Post { get; set; }
    public UsersModel User { get; set; }
}
