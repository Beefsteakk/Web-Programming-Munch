using System;
using System.ComponentModel.DataAnnotations;

namespace EffectiveWebProg.Models;

public class PostsEntity
{
    [Key]
    public Guid PostID { get; set; } // Primary Key
    [Required]
    public string PostTitle { get; set; }
    [Required]
    public string PostContent { get; set; }

    public PostsEntity()
    {
        PostID = Guid.NewGuid();
    }
}