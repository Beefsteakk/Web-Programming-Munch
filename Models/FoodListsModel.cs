using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EffectiveWebProg.Models;

public class FoodListsModel
{
    [Key]
    public Guid FoodListID { get; set; } // Primary Key

    [Required]
    public Guid OwnerID { get; set; } // Foreign Key From UsersEntity

    [Required, StringLength(100)]
    public string FoodListTitle { get; set; }

    [Required, StringLength(255)]
    public string FoodListDescription { get; set; }
    public DateTime FoodListCreatedAt { get; set; }


    public FoodListsModel()
    {
        FoodListID = Guid.NewGuid();
    }

    // Navigation properties
    public UsersModel Owner { get; set; }

    public ICollection<FoodListEntriesModel> FoodListEntry { get; set; }
    public ICollection<FoodListLikesModel> FoodListLike { get; set; }
}
