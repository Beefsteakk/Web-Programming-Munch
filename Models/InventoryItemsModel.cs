using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class InventoryItemsModel
{
    [Key, Column(Order = 0, TypeName = "char(36)")]
    public Guid InventoryID { get; set; } // Composite Primary Key and Foreign Key From InventoryModel

    [Key, Column(Order = 1, TypeName = "char(36)")]
    public Guid ItemID { get; set; } // Composite Primary Key and Foreign Key From ItemsModel

    public int StockCount { get; set; }

    public DateTime LastUpdated { get; set; }



    // Navigation properties
    [ForeignKey("InventoryID")]
    public required InventoryModel Inventory { get; set; }

    [ForeignKey("ItemID")]
    public required ItemsModel Items { get; set; }



    public InventoryItemsModel()
    {

    }
}
