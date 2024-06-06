using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class FoodListEntriesModel
{

    [Key, Column(Order = 0)]
    public Guid FoodListID { get; set; } // Composite Primary Key and Foreign Key From FoodListsModel

    [Key, Column(Order = 1)]
    public Guid RestID { get; set; } // Composite Primary Key and Foreign Key From RestaurantModel


    // Navigation properties
    [ForeignKey("FoodListID")]
    public required FoodListsModel FoodList { get; set; }
    [ForeignKey("RestID")]
    public required RestaurantsModel Restaurant { get; set; }
}
