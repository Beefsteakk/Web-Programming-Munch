using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class RestCategoryModel
{

    [Key, Column(Order = 0, TypeName = "char(36)")]
    public Guid RestID { get; set; } // Composite Primary Key and Foreign Key From RestaurantModel

    [Key, Column(Order = 1, TypeName = "char(36)")]
    public Guid CatID { get; set; } // Composite Primary Key and Foreign Key From CategoryModel


    // Navigation properties
    [ForeignKey("RestID")]
    public required RestaurantsModel Restaurant { get; set; }
    [ForeignKey("CatID")]
    public required CategoryModel Category { get; set; }
}
