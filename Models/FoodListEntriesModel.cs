using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EffectiveWebProg.Models;

public class FoodListEntriesModel
{
    [Key]
    public Guid FoodListEntriesID { get; set; } // Primary Key

    [Required]
    public Guid RestaurantID { get; set; } // Foreign Key From RestaurantModel
    [Required]
    public Guid FoodListID { get; set; } // Foreign Key From FoodListsModel



    public FoodListEntriesModel()
    {
        FoodListEntriesID = Guid.NewGuid();
    }

    // Navigation properties
    public FoodListsModel FoodList { get; set; }
    public RestaurantsModel Restaurant { get; set; }
}
