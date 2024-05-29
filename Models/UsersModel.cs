using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EffectiveWebProg.Models;

public class UsersModel
{
    [Key]
    public Guid UserID { get; set; } // Primary Key

    [Required, StringLength(100)]
    public required string UserName { get; set; }

    [Required, StringLength(255)]
    public required string UserEmail { get; set; }

    [Required, StringLength(50)]
    public required string UserUsername { get; set; }

    [Required, StringLength(255)]
    public required string UserPassword { get; set; }

    [StringLength(255)]
    public string? UserProfilePic { get; set; } // nullable

    [StringLength(255)] 
    public string? UserBio { get; set; } // nullable

    [Required, StringLength(255)]
    public required string UserAccountType { get; set; }
    public int? AccountToken { get; set; } // nullable
    public bool? AccountVerified { get; set; } // nullable
    public DateTime UserCreatedAt { get; set; }



    // Navigation properties
    public ICollection<PostsModel> Post { get; set; }
    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesModel> PostLike { get; set; }
    public RestaurantsModel? Restaurant { get; set; } // One-to-one relationship
    public ICollection<FoodListsModel> FoodList { get; set; }
    public ICollection<FoodListLikesModel> FoodListLike { get; set; }
    public ICollection<ReservationsModel> Reservation { get; set; }
    public ICollection<RestaurantRatingsModel> RestaurantRating { get; set; }


    public UsersModel()
    {
        UserID = Guid.NewGuid();
        UserCreatedAt = DateTime.UtcNow;
        Post = new List<PostsModel>();
        Comment = new List<CommentsModel>();
        PostLike = new List<PostLikesModel>();
        Restaurant = null; // Initialize to null for one-to-one relationship
        FoodList = new List<FoodListsModel>();
        FoodListLike = new List<FoodListLikesModel>();
        Reservation = new List<ReservationsModel>();
        RestaurantRating = new List<RestaurantRatingsModel>();
    }

}
