using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class FoodListsModel
{
    [Key]
    public Guid FoodListID { get; set; } // Primary Key

    [Required]
    public Guid OwnerID { get; set; } // Foreign Key From UsersEntity

    [Required, StringLength(100)]
    public required string FoodListTitle { get; set; }

    [Required, StringLength(255)]
    public required string FoodListDescription { get; set; }
    public DateTime FoodListCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("OwnerID")]
    public required UsersModel Owner { get; set; }

    public ICollection<FoodListEntriesModel> FoodListEntry { get; set; }
    public ICollection<FoodListLikesModel> FoodListLike { get; set; }

    public FoodListsModel()
    {
        FoodListID = Guid.NewGuid();
        FoodListCreatedAt = DateTime.UtcNow;
        FoodListEntry = new List<FoodListEntriesModel>();
        FoodListLike = new List<FoodListLikesModel>();
    }
}
