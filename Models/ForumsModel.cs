using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class ForumsModel
{
    [Key]
    public Guid ForumID { get; set; } // Primary Key

    [Required]
    public Guid UserID { get; set; } // Foreign Key From UsersModel

    [Required, StringLength(100)]
    public required string ForumName { get; set; }

    [Required, StringLength(255)]
    public required string ForumDesc { get; set; }
    public DateTime ForumCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }

    public ICollection<ForumVotesModel> ForumVote { get; set; }
    public ICollection<ForumCommentsModel> ForumComment { get; set; }

    public ForumsModel()
    {
        ForumID = Guid.NewGuid();
        ForumCreatedAt = DateTime.UtcNow;
        ForumVote = new List<ForumVotesModel>();
        ForumComment = new List<ForumCommentsModel>();
    }
}
