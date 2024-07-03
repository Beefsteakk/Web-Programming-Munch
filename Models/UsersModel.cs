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

    public int? AccountToken { get; set; } // nullable
    public bool? AccountVerified { get; set; } // nullable
    public DateTime UserCreatedAt { get; set; }
    

    // Navigation properties
    public ICollection<CommentsModel> Comment { get; set; }
    public ICollection<PostLikesModel> PostLike { get; set; }
    public ICollection<ReservationsModel> Reservation { get; set; }
    public ICollection<FollowingsModel> Followings { get; set; } 
    public ICollection<UserCardModel> UserCard { get; set; }



    public UsersModel()
    {
        UserID = Guid.NewGuid();
        UserCreatedAt = DateTime.UtcNow;
        Comment = new List<CommentsModel>();
        PostLike = new List<PostLikesModel>();
        Reservation = new List<ReservationsModel>();
        Followings = new List<FollowingsModel>();
        UserCard = new List<UserCardModel>();
    }
}
