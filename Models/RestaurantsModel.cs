using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models
{
    public class RestaurantsModel
    {
        [Key]
        [Column(TypeName = "char(36)")]
        public Guid RestID { get; set; } // Primary Key

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? RestName { get; set; } // nullable

        public double? RestLat { get; set; } // nullable
        public double? RestLong { get; set; } // nullable

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? RestAddress { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? RestEmail { get; set; } // nullable

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? RestPassword { get; set; } // nullable

        public int? RestContact { get; set; }

        [Column(TypeName = "text")]
        public string? RestBio { get; set; } // nullable

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? RestPic { get; set; } // nullable

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? RestWebsite { get; set; } // nullable

        public float? RestRatings { get; set; }
        public TimeSpan? RestOpenHr {get; set;}
        public TimeSpan? RestCloseHr {get; set;}

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? RestCoverPic { get; set; } // nullable

        // Navigation properties
        public ICollection<ReservationsModel> Reservation { get; set; }
        public ICollection<CommentsModel> Comment { get; set; }
        public ICollection<PostsModel> Post { get; set; }
        public ICollection<RestCardModel> RestCard { get; set; }
        public ICollection<InventoryModel> Inventory { get; set; }
        public ICollection<CartModel> Cart { get; set; }
        public ICollection<EmployeesModel> Employee { get; set; }
        public ICollection<FollowingsModel> Followings { get; set; }

        public RestaurantsModel()
        {
            RestID = Guid.NewGuid();
            Reservation = new List<ReservationsModel>();
            Comment = new List<CommentsModel>();
            Post = new List<PostsModel>();
            RestCard = new List<RestCardModel>();
            Inventory = new List<InventoryModel>();
            Cart = new List<CartModel>();
            Employee = new List<EmployeesModel>();
        }
    }
}
