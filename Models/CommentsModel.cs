using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class CommentsModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid CommentID { get; set; } // Primary Key

    [Required]
    [Column(TypeName = "char(36)")]
    public Guid PostID { get; set; } // Foreign Key From PostsModel
    
    [Column(TypeName = "char(36)")]
    public Guid? UserID { get; set; } // Foreign Key From UsersModel

    [Column(TypeName = "char(36)")]
    public Guid? RestID { get; set; } // Foreign Key From RestaurantsModel

    [Required]
    [Column(TypeName = "text")]
    public string CommentContent { get; set; }

    public DateTime CommentCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("PostID")]
    public PostsModel? Post { get; set; }

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