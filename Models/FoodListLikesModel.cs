using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class FoodListLikesModel
{
    [Key, Column(Order = 0)]
    public Guid FoodListID { get; set; } // Composite Primary Key and Foreign Key From FoodListsModel

    [Key, Column(Order = 1)]
    public Guid UserID { get; set; } // Composite Primary Key and Foreign Key From UsersModel

    [Required]
    public DateTime LikeCreatedAt { get; set; }




    // Navigation properties
    [ForeignKey("FoodListID")]
    public required FoodListsModel FoodList { get; set; }

    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }


    public FoodListLikesModel()
    {
        LikeCreatedAt = DateTime.UtcNow;
    }
}
