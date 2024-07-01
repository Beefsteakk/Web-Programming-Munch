using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;
using Org.BouncyCastle.Asn1.Cms;

public class InventoryModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid InventoryID { get; set; } // Primary Key
    
    [Required]
    [Column(TypeName = "char(36)")]
    public Guid RestID { get; set; } // Foreign Key From RestaurantsModel

    [StringLength(255)] 
    [Column(TypeName = "varchar(255)")]
    public string? InventoryName { get; set; }



    // Navigation properties
    [ForeignKey("RestID")]
    public required RestaurantsModel Rest { get; set; }


    public ICollection<InventoryItemsModel> InventoryItem { get; set; } 


    public InventoryModel()
    {
        InventoryID = Guid.NewGuid();
        InventoryItem = new List<InventoryItemsModel>();
    }

}
