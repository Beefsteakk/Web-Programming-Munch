using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EffectiveWebProg.Models;

namespace EffectiveWebProg.Models;

public class CategoryModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid CatID { get; set; } // Primary Key

    [Required, StringLength(50)]
    [Column(TypeName = "varchar(255)")]
    public required string CatType { get; set; }



    // Navigation properties
    public ICollection<FoodListCategoryModel> FoodListCat { get; set; }
    public ICollection<RestCategoryModel> RestCat { get; set; }

    public CategoryModel()
    {
        CatID = Guid.NewGuid();
        FoodListCat = new List<FoodListCategoryModel>();
        RestCat = new List<RestCategoryModel>();
    }
}
