using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;
using Org.BouncyCastle.Asn1.Cms;
namespace EffectiveWebProg.Models;


public class ReservationsModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid ReservationID { get; set; } // Primary Key
    
    [Required]
    [Column(TypeName = "char(36)")]
    public Guid UserID { get; set; } // Foreign Key From UsersModel

    [Required]
    [Column(TypeName = "char(36)")]
    public Guid RestID { get; set; } // Foreign Key From RestaurantModel

    [Required]
    public int NumOfGuests { get; set; }
    
    [StringLength(255)] 
    [Column(TypeName = "varchar(255)")]
    public string? SpecialRequest { get; set; }
    
    [StringLength(255)] 
    [Column(TypeName = "varchar(255)")]
    public string? ReservationStatus { get; set; }

    [Required]
    public DateOnly ReservationDate { get; set; }

    [Required]
    public TimeSpan ReservationTime { get; set; }



    [StringLength(255)]
    [Column(TypeName = "varchar(255)")] 
    public string? ReservedName { get; set; }


    // Navigation properties
    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }
    
    [ForeignKey("RestID")]
    public required RestaurantsModel Restaurant { get; set; }


    public ReservationsModel()
    {
        ReservationID = Guid.NewGuid();
    }

}
