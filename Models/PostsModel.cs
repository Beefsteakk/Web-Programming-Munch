using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class PostsModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid PostID { get; set; } // Primary Key

    [Column(TypeName = "char(36)")]
    public Guid? UserID { get; set; } // Foreign Key From UsersModel

    [Column(TypeName = "char(36)")]
    public Guid? RestID { get; set; } // Foreign Key From RestaurantsModel
    
    [Required]
    [Column(TypeName = "varchar(255)")]
    public string PostTitle { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(255)")]
    public string PostContent { get; set; }
    
    [StringLength(255)]
    [Column(TypeName = "varchar(255)")]
    public string? PostImageURL { get; set; }
    public DateTime PostCreatedAt { get; set; }

    [Column(TypeName = "char(36)")]
    public Guid? TaggedRest { get; set; } // Foreign Key From RestaurantsModel


    // Navigation properties
    [ForeignKey("UserID")]
    public UsersModel? User { get; set; }
    [ForeignKey("RestID")]
    public RestaurantsModel? Restaurant { get; set; }
    [ForeignKey("TaggedRest")]
    public RestaurantsModel? TaggedRestaurant { get; set; }

    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesUserModel> PostLikeUser { get; set; }
    public ICollection<PostLikesRestModel> PostLikeRest { get; set; }
    public ICollection<ReviewsModel> Review { get; set; }


    public PostsModel()
    {
        PostID = Guid.NewGuid();
        PostCreatedAt = DateTime.UtcNow;
        Comment = new List<CommentsModel>();
        PostLikeUser = new List<PostLikesUserModel>();
        PostLikeRest = new List<PostLikesRestModel>();
        Review = new List<ReviewsModel>();
    }
}