<<<<<<< HEAD
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class RestaurantsModel
{
    [Key]
    public Guid RestID { get; set; } // Primary Key

    [StringLength(255)]
    public string? RestName { get; set; } // nullable

    public double? RestLat { get; set; } // nullable
    public double? RestLong { get; set; } // nullable

    [StringLength(255)]
    public string? RestAddress { get; set; } 

    [StringLength(255)]
    public string? RestEmail { get; set; } // nullable

    [StringLength(255)]
    public string? RestPassword { get; set; } // nullable

    public int? RestContact {get; set;}

    public string? RestBio { get; set; } // nullable

    [StringLength(255)]
    public string? RestPic { get; set; } // nullable

    [StringLength(255)]
    public string? RestWebsite { get; set; } // nullable

    public float? RestRatings {get; set;}
    

    // Navigation properties
    public ICollection<FoodListEntriesModel> FoodEntry { get; set; }
    public ICollection<ReservationsModel> Reservation { get; set; }
    public ICollection<RestaurantReviewsModel> RestaurantReview { get; set; }
    public ICollection<RestaurantFollowingsModel> FollowedBy { get; set; } 
    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesRestModel> PostLikesRest { get; set; }
    public ICollection<PostsModel> Post { get; set; }
    public ICollection<ForumCommentsModel> ForumComment { get; set; }
    

    public RestaurantsModel()
    {
        RestID = Guid.NewGuid();
        FoodEntry = new List<FoodListEntriesModel>();
        Reservation = new List<ReservationsModel>();
        RestaurantReview = new List<RestaurantReviewsModel>();
        FollowedBy = new List<RestaurantFollowingsModel>();
        Comment = new List<CommentsModel>();
        PostLikesRest = new List<PostLikesRestModel>();
        Post = new List<PostsModel>();
        ForumComment = new List<ForumCommentsModel>();
    }

}
=======
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class RestaurantsModel
{
    [Key]
    public Guid RestID { get; set; } // Primary Key

    [StringLength(255)]
    public string? RestName { get; set; } // nullable

    public double? RestLat { get; set; } // nullable
    public double? RestLong { get; set; } // nullable

    [StringLength(255)]
    public string? RestAddress { get; set; } 

    [StringLength(255)]
    public string? RestEmail { get; set; } // nullable

    [StringLength(255)]
    public string? RestPassword { get; set; } // nullable

    public int? RestContact {get; set;}

    public string? RestBio { get; set; } // nullable

    [StringLength(255)]
    public string? RestPic { get; set; } // nullable

    [StringLength(255)]
    public string? RestWebsite { get; set; } // nullable

    public float? RestRatings {get; set;}
    

    // Navigation properties
    public ICollection<FoodListEntriesModel> FoodEntry { get; set; }
    public ICollection<ReservationsModel> Reservation { get; set; }
    public ICollection<RestaurantReviewsModel> RestaurantReview { get; set; }
    public ICollection<RestaurantFollowingsModel> FollowedBy { get; set; } 
    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesRestModel> PostLikesRest { get; set; }
    public ICollection<PostsModel> Post { get; set; }
    public ICollection<ForumCommentsModel> ForumComment { get; set; }
    

    public RestaurantsModel()
    {
        RestID = Guid.NewGuid();
        FoodEntry = new List<FoodListEntriesModel>();
        Reservation = new List<ReservationsModel>();
        RestaurantReview = new List<RestaurantReviewsModel>();
        FollowedBy = new List<RestaurantFollowingsModel>();
        Comment = new List<CommentsModel>();
        PostLikesRest = new List<PostLikesRestModel>();
        Post = new List<PostsModel>();
        ForumComment = new List<ForumCommentsModel>();
    }

}
>>>>>>> origin/Deva
