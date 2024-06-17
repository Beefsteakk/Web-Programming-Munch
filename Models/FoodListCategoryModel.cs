using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class FoodListCategoryModel
{

    [Key, Column(Order = 0, TypeName = "char(36)")]
    public Guid FoodListID { get; set; } // Composite Primary Key and Foreign Key From FoodListsModel

    [Key, Column(Order = 1, TypeName = "char(36)")]
    public Guid CatID { get; set; } // Composite Primary Key and Foreign Key From CategoryModel


    // Navigation properties
    [ForeignKey("FoodListID")]
    public required FoodListsModel FoodList { get; set; }
    [ForeignKey("CatID")]
    public required CategoryModel Category { get; set; }
}