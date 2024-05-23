using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EffectiveWebProg.Models;

public class UsersModel
{
    [Key]
    public Guid UserID { get; set; } // Primary Key

    [Required, StringLength(100)]
    public string UserName { get; set; }

    [Required, StringLength(255)]
    public string UserEmail { get; set; }

    [Required, StringLength(50)]
    public string UserUsername { get; set; }

    [Required, StringLength(255)]
    public string UserPassword { get; set; }

    [StringLength(255)]
    public string UserProfilePic { get; set; }

    [StringLength(255)] 
    public string UserBio { get; set; }

    [Required, StringLength(255)]
    public string UserAccountType { get; set; }

    public DateTime UserCreatedAt { get; set; }


    public UsersModel()
    {
        UserID = Guid.NewGuid();
    }

    // Navigation properties
    public ICollection<PostsModel> Post { get; set; }
    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesModel> PostLike { get; set; }
    public ICollection<RestaurantsModel> Restaurant { get; set; }
    public ICollection<FoodListsModel> FoodList { get; set; }
    public ICollection<FoodListLikesModel> FoodListLike { get; set; }
    public ICollection<ReservationsModel> Reservation { get; set; }
    public ICollection<RestaurantRatingsModel> RestaurantRating { get; set; }
}
