using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;


public class PostsModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid PostID { get; set; } // Primary Key

    [Column(TypeName = "char(36)")]
    public Guid? RestID { get; set; } // Foreign Key From RestaurantsModel
    
    [Required]
    [Column(TypeName = "varchar(255)")]
    public string PostContent { get; set; }
    
    public DateTime PostCreatedAt { get; set; }

    // Navigation properties
    [ForeignKey("RestID")]
    public RestaurantsModel? Restaurant { get; set; }

    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesModel> PostLike { get; set; }
    public ICollection<PostPicsModel> PostPic { get; set; }


    public PostsModel()
    {
        PostID = Guid.NewGuid();
        PostCreatedAt = DateTime.UtcNow;
        Comment = new List<CommentsModel>();
        PostLike = new List<PostLikesModel>();
        PostPic = new List<PostPicsModel>();
    }
}