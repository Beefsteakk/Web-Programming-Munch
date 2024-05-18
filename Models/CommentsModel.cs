using System;
using System.ComponentModel.DataAnnotations;

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
    public string Comments { get; set; }

    public DateTime CommentCreatedAt { get; set; }


    public CommentsModel()
    {
        CommentID = Guid.NewGuid();
    }

    // Navigation properties
    public PostsModel Post { get; set; }
    public UsersModel Author { get; set; }
}