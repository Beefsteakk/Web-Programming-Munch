using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;
using Org.BouncyCastle.Asn1.Cms;

public class ItemsModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid ItemID { get; set; } // Primary Key
    
    [Required]
    [Column(TypeName = "char(36)")]
    public Guid CatID { get; set; } // Foreign Key From ItemCatModel

    [StringLength(255)] 
    [Column(TypeName = "varchar(255)")]
    public string? ItemName { get; set; }

    [StringLength(255)]
    [Column(TypeName = "varchar(255)")] 
    public string? ItemPic { get; set; }


    // Navigation properties
    [ForeignKey("CatID")]
    public required ItemCatModel ItemCat { get; set; }


    public ICollection<InventoryItemsModel> InventoryItem { get; set; } 
    public ICollection<CartItemsModel> CartItem { get; set; } 


    public ItemsModel()
    {
        ItemID = Guid.NewGuid();
        InventoryItem = new List<InventoryItemsModel>();
        CartItem = new List<CartItemsModel>();
    }

}
