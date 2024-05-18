using System;
using System.ComponentModel.DataAnnotations;

namespace EffectiveWebProg.Models;

public class PostsModel
{
    [Key]
    public Guid PostID { get; set; } // Primary Key
    [Required]
    public Guid AuthorID { get; set; } // Foreign Key From UsersEntity
    [Required]
    public string PostTitle { get; set; }
    [Required, StringLength(255)]
    public string PostContent { get; set; }
    [StringLength(255)]
    public string PostImageURL { get; set; }
    [StringLength(255)]
    public string PostLocation { get; set; }

    public DateTime PostCreatedAt { get; set; }
    public DateTime PostUpdatedAt { get; set; }

    public PostsModel()
    {
        PostID = Guid.NewGuid();
    }


    // Navigation properties
    public UsersModel Author { get; set; }
    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesModel> PostLike { get; set; }
}