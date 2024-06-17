using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;

public class SearchHistoryModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid SearchID { get; set; } // Primary Key
    
    [Required]
    [Column(TypeName = "char(36)")]
    public Guid UserID { get; set; } // Foreign Key From UsersModel

    [Required]
    [Column(TypeName = "varchar(255)")]
    public required string UserSearch { get; set; }
    public DateTime SearchCreatedAt { get; set; }


    // Navigation properties
    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }


    public SearchHistoryModel()
    {
        SearchID = Guid.NewGuid();
        SearchCreatedAt = DateTime.UtcNow;
    }

}
