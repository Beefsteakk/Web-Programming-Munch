using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class CartItemsModel
{
    [Key, Column(Order = 0, TypeName = "char(36)")]
    public Guid CartID { get; set; } // Composite Primary Key and Foreign Key From CartModel

    [Key, Column(Order = 1, TypeName = "char(36)")]
    public Guid ItemID { get; set; } // Composite Primary Key and Foreign Key From ItemsModel

    public int Quantity { get; set; }



    // Navigation properties
    [ForeignKey("CartID")]
    public required CartModel Cart { get; set; }

    [ForeignKey("ItemID")]
    public required ItemsModel Items { get; set; }



    public CartItemsModel()
    {

    }
}
