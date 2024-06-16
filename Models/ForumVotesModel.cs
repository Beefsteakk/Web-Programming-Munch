using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;

public class ForumVotesModel
{
    [Key, Column(Order = 0, TypeName = "char(36)")]
    public Guid ForumID { get; set; } // Composite Primary Key and Foreign Key From ForumsModel

    [Key, Column(Order = 1, TypeName = "char(36)")]
    public Guid UserID { get; set; } // Composite Primary Key and Foreign Key From UsersModel

    [Required]
    public bool VoteType { get; set; }

    // Navigation properties
    [ForeignKey("ForumID")]
    public required ForumsModel Forum { get; set; }

    [ForeignKey("UserID")]
    public required UsersModel User { get; set; }
}
