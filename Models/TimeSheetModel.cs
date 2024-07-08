using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;
using Org.BouncyCastle.Asn1.Cms;
namespace EffectiveWebProg.Models;


public class TimeSheetModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid SheetID { get; set; } // Primary Key
    
    [Required]
    [Column(TypeName = "char(36)")]
    public Guid EmployeeID { get; set; } // Foreign Key From EmployeesModel

    [StringLength(255)] 
    [Column(TypeName = "varchar(50)")]
    public string? Day { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }



    // Navigation properties
    [ForeignKey("EmployeeID")]
    public required EmployeesModel Employees { get; set; }


    public TimeSheetModel()
    {
        SheetID = Guid.NewGuid();
    }

}
