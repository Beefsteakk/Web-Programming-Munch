using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;
using Org.BouncyCastle.Asn1.Cms;
namespace EffectiveWebProg.Models;
public class CartModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid CartID { get; set; } // Primary Key
    
    [Required]
    [Column(TypeName = "char(36)")]
    public Guid RestID { get; set; } // Foreign Key From RestaurantsModel

    [StringLength(255)] 
    [Column(TypeName = "varchar(255)")]
    public string? CartName { get; set; }

    public DateTime CreatedAt { get; set; }

    [StringLength(255)] 
    [Column(TypeName = "varchar(20)")]
    public string? Status { get; set; }

    public double CartTotal { get; set; }


    // Navigation properties
    [ForeignKey("RestID")]
    public required RestaurantsModel Rest { get; set; }


    public ICollection<CartItemsModel> CartItem { get; set; } 


    public CartModel()
    {
        CartID = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        CartItem = new List<CartItemsModel>();
    }

}
