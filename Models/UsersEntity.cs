using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;

public class UsersEntity
{
    [Key]
    public Guid UserID { get; set; } // Primary Key

    [Required]
    [StringLength(100)]
    public string UserName { get; set; }

    [Required]
    [StringLength(255)]
    public string UserEmail { get; set; }

    [Required]
    [StringLength(100)]
    public string UserUsername { get; set; }

    [Required]
    [StringLength(255)]
    public string UserPassword { get; set; }
    
    [Required]
    [StringLength(255)]
    public string UserProfilePic { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string UserBio { get; set; }
    
    [Required]
    [StringLength(50)]
    public string UserAccountType { get; set; }

    [Required]
    public DateTime UserCreatedAt { get; set; }

    public UsersEntity()
    {
        UserID = Guid.NewGuid();
    }
}