using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class ReviewsModel
{
    [Key]
    public Guid ReviewID { get; set; } // Primary Key
    
    [Required]
    public Guid PostID { get; set; } // Foreign Key From PostsModel

    [Required]
    public int RatingValue { get; set; }

    public string? ReviewComments {get; set;}
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
