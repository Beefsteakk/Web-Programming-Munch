using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;
namespace EffectiveWebProg.Models;


public class UserCardModel
{
    [Key, Column(Order = 0, TypeName = "char(36)")]
    public Guid CardID { get; set; } // Composite Primary Key and Foreign Key From CreditCardModel

    [Key, Column(Order = 1, TypeName = "char(36)")]
    public Guid UserID { get; set; } // Composite Primary Key and Foreign Key From UsersModel



    // Navigation properties
    [ForeignKey("CardID")]
    public required CreditCardModel Card { get; set; }

    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }



    public UserCardModel()
    {

    }
}
