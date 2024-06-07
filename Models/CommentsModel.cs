using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class CommentsModel
{
    [Key]
    public Guid CommentID { get; set; } // Primary Key

    [Required]
    public Guid PostID { get; set; } // Foreign Key From PostsModel

    public Guid? UserID { get; set; } // Foreign Key From UsersModel

    public Guid? RestID { get; set; } // Foreign Key From RestaurantsModel

    [Required]
    public required string Comments { get; set; }

    public DateTime CommentCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("PostID")]
    public required PostsModel Post { get; set; }

    [ForeignKey("UserID")]
    public UsersModel? User { get; set; }

    [ForeignKey("RestID")]
    public RestaurantsModel? Restaurant { get; set; }

    public CommentsModel()
    {
        CommentID = Guid.NewGuid();
        CommentCreatedAt = DateTime.UtcNow;
    }
}