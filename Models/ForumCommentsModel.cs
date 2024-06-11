using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class ForumCommentsModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid CommentID { get; set; } // Primary Key

    [Required]
    [Column(TypeName = "char(36)")]
    public Guid ForumID { get; set; } // Foreign Key From ForumsModel

    [Column(TypeName = "char(36)")]
    public Guid? UserID { get; set; } // Foreign Key From UsersModel

    [Column(TypeName = "char(36)")]
    public Guid? RestID { get; set; } // Foreign Key From RestaurantsModel

    [Column(TypeName = "Text")]
    [Required]
    public required string Comments { get; set; }



    // Navigation properties
    [ForeignKey("ForumID")]
    public required ForumsModel Forum { get; set; }

    [ForeignKey("UserID")]
    public UsersModel? User { get; set; }

    [ForeignKey("RestID")]
    public RestaurantsModel? Restaurant { get; set; }

    public ForumCommentsModel()
    {
        CommentID = Guid.NewGuid();
    }
}