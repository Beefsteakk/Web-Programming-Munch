<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class RestaurantReviewsModel
{
    [Key]
    public Guid ReviewID { get; set; } // Primary Key
    
    [Required]
    public Guid UserID { get; set; } // Foreign Key From UsersModel

    [Required]
    public Guid RestID { get; set; } // Foreign Key From RestaurantsModel

    [Required]
    public int RatingValue { get; set; }

    public string? Comments {get; set;}
    public DateTime ReviewCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }

    [ForeignKey("RestID")]
    public required RestaurantsModel Restaurant { get; set; }


    public RestaurantReviewsModel()
    {
        ReviewID = Guid.NewGuid();
        ReviewCreatedAt = DateTime.UtcNow;
    }
}
=======
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class RestaurantReviewsModel
{
    [Key]
    public Guid ReviewID { get; set; } // Primary Key
    
    [Required]
    public Guid UserID { get; set; } // Foreign Key From UsersModel

    [Required]
    public Guid RestID { get; set; } // Foreign Key From RestaurantsModel

    [Required]
    public int RatingValue { get; set; }

    public string? Comments {get; set;}
    public DateTime ReviewCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }

    [ForeignKey("RestID")]
    public required RestaurantsModel Restaurant { get; set; }


    public RestaurantReviewsModel()
    {
        ReviewID = Guid.NewGuid();
        ReviewCreatedAt = DateTime.UtcNow;
    }
}
>>>>>>> origin/Deva
