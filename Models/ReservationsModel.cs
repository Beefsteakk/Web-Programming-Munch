using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class ReservationsModel
{
    [Key]
    public Guid ReservationID { get; set; } // Primary Key
    
    [Required]
    public Guid UserID { get; set; } // Foreign Key From UsersModel

    [Required]
    public Guid RestID { get; set; } // Foreign Key From RestaurantModel

    public DateTime ReservationTime { get; set; }

    [Required]
    public int NumOfGuests { get; set; }
    [StringLength(255)] 
    public string? SpecialRequest { get; set; }
    [StringLength(50)] 
    public string? ReservationStatus { get; set; }

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
