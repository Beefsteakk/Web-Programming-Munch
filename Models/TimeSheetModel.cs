using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    
    [Required]
    public DateTime Day { get; set; }

    [Required]
    [Column(TypeName = "varchar(20)")]
    public string ShiftType { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    // Navigation properties
    [ForeignKey("EmployeeID")]
    [JsonIgnore]
    public EmployeesModel? Employees { get; set; }

    public TimeSheetModel()
    {
        SheetID = Guid.NewGuid();
    }

}
