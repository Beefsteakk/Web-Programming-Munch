using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;
namespace EffectiveWebProg.Models;


public class RestCardModel
{
    [Key, Column(Order = 0, TypeName = "char(36)")]
    public Guid CardID { get; set; } // Composite Primary Key and Foreign Key From CreditCardModel

    [Key, Column(Order = 1, TypeName = "char(36)")]
    public Guid RestID { get; set; } // Composite Primary Key and Foreign Key From RestaurantsModel



    // Navigation properties
    [ForeignKey("CardID")]
    public required CreditCardModel Card { get; set; }

    [ForeignKey("RestID")]
    public required RestaurantsModel Rest { get; set; }



    public RestCardModel()
    {

    }
}
