using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;
using Org.BouncyCastle.Asn1.Cms;

public class RestViewHistoryModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid ViewID { get; set; } // Primary Key
    
    [Required]
    [Column(TypeName = "char(36)")]
    public Guid UserID { get; set; } // Foreign Key From UsersModel

    [Required]
    [Column(TypeName = "char(36)")]
    public Guid RestID { get; set; } // Foreign Key From RestaurantModel

    public DateTime ViewedAt { get; set; }


    // Navigation properties
    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }
    
    [ForeignKey("RestID")]
    public required RestaurantsModel Restaurant { get; set; }


    public RestViewHistoryModel()
    {
        ViewID = Guid.NewGuid();
        ViewedAt = DateTime.UtcNow;
    }

}
