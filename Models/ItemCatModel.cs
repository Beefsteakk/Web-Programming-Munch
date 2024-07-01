using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EffectiveWebProg.Models;

namespace EffectiveWebProg.Models;

public class ItemCatModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid CatID { get; set; } // Primary Key

    [Required, StringLength(100)]
    [Column(TypeName = "varchar(255)")]
    public required string CatName { get; set; }

    

    // Navigation properties
    public ICollection<ItemsModel> Item { get; set; }



    public ItemCatModel()
    {
        CatID = Guid.NewGuid();
        Item = new List<ItemsModel>();
    }
}
