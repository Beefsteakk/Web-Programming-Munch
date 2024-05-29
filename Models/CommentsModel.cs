using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class CommentsModel
{
    [Key]
    public Guid CommentID { get; set; } // Primary Key

    [Required]
    public Guid PostID { get; set; } // Foreign Key From PostsEntity

    [Required]
    public Guid AuthorID { get; set; } // Foreign Key From UsersEntity

    [Required]
    public required string Comments { get; set; }

    public DateTime CommentCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("PostID")]
    public required PostsModel Post { get; set; }

    [ForeignKey("AuthorID")]
    public required UsersModel Author { get; set; }

    public CommentsModel()
    {
        CommentID = Guid.NewGuid();
        CommentCreatedAt = DateTime.UtcNow;
    }
}