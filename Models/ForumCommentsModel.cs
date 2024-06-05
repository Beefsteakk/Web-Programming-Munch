using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class ForumCommentsModel
{
    [Key]
    public Guid CommentID { get; set; } // Primary Key

    [Required]
    public Guid ForumID { get; set; } // Foreign Key From ForumsModel

    public Guid? UserID { get; set; } // Foreign Key From UsersModel

    public Guid? RestID { get; set; } // Foreign Key From RestaurantsModel

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