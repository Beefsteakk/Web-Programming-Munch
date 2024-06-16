using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EffectiveWebProg.Models;

public class ReviewsModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid ReviewID { get; set; } // Primary Key
    
    [Required]
    [Column(TypeName = "char(36)")]
    public Guid PostID { get; set; } // Foreign Key From PostsModel

    [Required]
    public int RatingValue { get; set; }

    [Column(TypeName = "text")]
    public string? ReviewComments { get; set; }
    
    public DateTime ReviewCreatedAt { get; set; }

    // Navigation properties
    [ForeignKey("PostID")]
    public required PostsModel Post { get; set; }

    public ReviewsModel()
    {
        ReviewID = Guid.NewGuid();
        ReviewCreatedAt = DateTime.UtcNow;
    }
}
