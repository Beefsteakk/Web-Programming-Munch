using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EffectiveWebProg.Models;

namespace EffectiveWebProg.Models;

public class CreditCardModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid CardID { get; set; } // Primary Key

    public int CardNumber { get; set; }

    [Required, StringLength(100)]
    [Column(TypeName = "varchar(255)")]
    public required string CardName { get; set; }

    public int CardCSV { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public double CardBalance { get; set; }
    

    // Navigation properties
    public ICollection<UserCardModel> UserCard { get; set; }
    public ICollection<RestCardModel> RestCard { get; set; }



    public CreditCardModel()
    {
        CardID = Guid.NewGuid();
        UserCard = new List<UserCardModel>();
        RestCard = new List<RestCardModel>();
    }
}
