using System;
using System.Collections.Generic;
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

        // Navigation properties
        public ICollection<FoodListEntriesModel> FoodEntry { get; set; }
        public ICollection<ReservationsModel> Reservation { get; set; }
        public ICollection<RestaurantFollowingsModel> FollowedBy { get; set; }
        public ICollection<CommentsModel> Comment { get; set; }
        public ICollection<PostLikesRestModel> PostLikesRest { get; set; }
        public ICollection<PostsModel> Post { get; set; }
        public ICollection<ForumCommentsModel> ForumComment { get; set; }
        public ICollection<RestViewHistoryModel> RestViewHistory { get; set; }
        public ICollection<RestCategoryModel> RestCat { get; set; }

        public RestaurantsModel()
        {
            RestID = Guid.NewGuid();
            FoodEntry = new List<FoodListEntriesModel>();
            Reservation = new List<ReservationsModel>();
            FollowedBy = new List<RestaurantFollowingsModel>();
            Comment = new List<CommentsModel>();
            PostLikesRest = new List<PostLikesRestModel>();
            Post = new List<PostsModel>();
            ForumComment = new List<ForumCommentsModel>();
            RestViewHistory = new List<RestViewHistoryModel>();
            RestCat = new List<RestCategoryModel>();
        }
    }
}
