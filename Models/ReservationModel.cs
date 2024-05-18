using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EffectiveWebProg.Models;

public class ReservationModel
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
    public string SpecialRequest { get; set; }
    [StringLength(50)] 
    public string ReservationStatus { get; set; }



    public ReservationModel()
    {
        ReservationID = Guid.NewGuid();
    }

    // Navigation properties
    public UsersModel User { get; set; }
    public RestaurantsModel Restaurant { get; set; }
}
