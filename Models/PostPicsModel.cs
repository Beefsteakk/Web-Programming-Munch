using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EffectiveWebProg.Models;

public class PostPicsModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid PicID { get; set; } // Primary Key
    
    [Column(TypeName = "char(36)")]
    public Guid? PostID { get; set; } // Foreign Key From PostsModel


    [StringLength(255)]
    public required string ImageURL { get; set; }

    // Navigation properties
    [ForeignKey("PostID")]
    public required PostsModel Post { get; set; }

    public PostPicsModel()
    {
        PicID = Guid.NewGuid();
    }
}
