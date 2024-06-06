<<<<<<< HEAD
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class PostsModel
{
    [Key]
    public Guid PostID { get; set; } // Primary Key
    public Guid? UserID { get; set; } // Foreign Key From UsersModel
    public Guid? RestID { get; set; } // Foreign Key From RestaurantsModel
    [Required]
    public required string PostTitle { get; set; }
    [Required, StringLength(255)]
    public required string PostContent { get; set; }
    [StringLength(255)]
    public string? PostImageURL { get; set; }
    [StringLength(255)]
    public string? PostLocation { get; set; }

    public DateTime PostCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("UserID")]
    public UsersModel? User { get; set; }
    [ForeignKey("RestID")]
    public RestaurantsModel? Restaurant { get; set; }

    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesUserModel> PostLikeUser { get; set; }
    public ICollection<PostLikesRestModel> PostLikeRest { get; set; }


    public PostsModel()
    {
        PostID = Guid.NewGuid();
        PostCreatedAt = DateTime.UtcNow;
        Comment = new List<CommentsModel>();
        PostLikeUser = new List<PostLikesUserModel>();
        PostLikeRest = new List<PostLikesRestModel>();
    }
=======
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class PostsModel
{
    [Key]
    public Guid PostID { get; set; } // Primary Key
    public Guid? UserID { get; set; } // Foreign Key From UsersModel
    public Guid? RestID { get; set; } // Foreign Key From RestaurantsModel
    [Required]
    public required string PostTitle { get; set; }
    [Required, StringLength(255)]
    public required string PostContent { get; set; }
    [StringLength(255)]
    public string? PostImageURL { get; set; }
    [StringLength(255)]
    public string? PostLocation { get; set; }

    public DateTime PostCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("UserID")]
    public UsersModel? User { get; set; }
    [ForeignKey("RestID")]
    public RestaurantsModel? Restaurant { get; set; }

    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesUserModel> PostLikeUser { get; set; }
    public ICollection<PostLikesRestModel> PostLikeRest { get; set; }


    public PostsModel()
    {
        PostID = Guid.NewGuid();
        PostCreatedAt = DateTime.UtcNow;
        Comment = new List<CommentsModel>();
        PostLikeUser = new List<PostLikesUserModel>();
        PostLikeRest = new List<PostLikesRestModel>();
    }
>>>>>>> origin/Deva
}