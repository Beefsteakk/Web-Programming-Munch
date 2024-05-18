using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EffectiveWebProg.Models;

public class FoodListLikesModel
{
    [Key]
    public Guid FoodListLikeID { get; set; } // Primary Key

    [Required]
    public Guid FoodListID { get; set; } // Foreign Key From FoodListsModel

    [Required]
    public Guid UserID { get; set; } // Foreign Key From UsersModel


    public FoodListLikesModel()
    {
        FoodListLikeID = Guid.NewGuid();
    }

    // Navigation properties
    public FoodListsModel FoodList { get; set; }
    public UsersModel User { get; set; }
}
