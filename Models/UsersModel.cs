using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EffectiveWebProg.Models;

namespace EffectiveWebProg.Models;

public class UsersModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid UserID { get; set; } // Primary Key

    [Required, StringLength(100)]
    [Column(TypeName = "varchar(100)")]
    public required string UserName { get; set; }

    [Required, StringLength(255)]
    [Column(TypeName = "varchar(255)")]
    public required string UserEmail { get; set; }

    public int UserContactNum { get; set; }

    [Required, StringLength(50)]
    [Column(TypeName = "varchar(50)")]
    public required string UserUsername { get; set; }

    [Required, StringLength(255)]
    [Column(TypeName = "varchar(255)")]
    public required string UserPassword { get; set; }

    [StringLength(255)]
    [Column(TypeName = "varchar(255)")]
    public string? UserProfilePic { get; set; } // nullable

    [StringLength(255)]
    [Column(TypeName = "varchar(255)")]
    public string? UserBio { get; set; } // nullable

    public int? AccountToken { get; set; } // nullable
    public bool? AccountVerified { get; set; } // nullable
    public DateTime UserCreatedAt { get; set; }

    // Navigation properties
    public ICollection<PostsModel> Post { get; set; }
    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesUserModel> PostLikeUser { get; set; }
    public ICollection<FoodListsModel> FoodList { get; set; }
    public ICollection<FoodListLikesModel> FoodListLike { get; set; }
    public ICollection<ReservationsModel> Reservation { get; set; }
    public ICollection<UserFollowingsModel> Followings { get; set; } // Users that this user is following
    public ICollection<UserFollowingsModel> Followers { get; set; } // Users that follow this user
    public ICollection<RestaurantFollowingsModel> RestaurantFollowings { get; set; }
    public ICollection<ForumsModel> Forum { get; set; }
    public ICollection<ForumVotesModel> ForumVote { get; set; }
    public ICollection<ForumCommentsModel> ForumComment { get; set; }
    public ICollection<SearchHistoryModel> SearchHistory { get; set; }
    public ICollection<RestViewHistoryModel> RestViewHistory { get; set; }

    public UsersModel()
    {
        UserID = Guid.NewGuid();
        UserCreatedAt = DateTime.UtcNow;
        Post = new List<PostsModel>();
        Comment = new List<CommentsModel>();
        PostLikeUser = new List<PostLikesUserModel>();
        FoodList = new List<FoodListsModel>();
        FoodListLike = new List<FoodListLikesModel>();
        Reservation = new List<ReservationsModel>();
        Followings = new List<UserFollowingsModel>();
        Followers = new List<UserFollowingsModel>();
        RestaurantFollowings = new List<RestaurantFollowingsModel>();
        Forum = new List<ForumsModel>();
        ForumVote = new List<ForumVotesModel>();
        ForumComment = new List<ForumCommentsModel>();
        SearchHistory = new List<SearchHistoryModel>();
        RestViewHistory = new List<RestViewHistoryModel>();
    }
}
