using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using EffectiveWebProg.Models;
using Org.BouncyCastle.Asn1.Cms;

public class EmployeesModel
{
    [Key]
    [Column(TypeName = "char(36)")]
    public Guid EmployeeID { get; set; } // Primary Key
    
    [Required]
    [Column(TypeName = "char(36)")]
    public Guid RestID { get; set; } // Foreign Key From RestaurantModel

    [StringLength(255)] 
    [Column(TypeName = "varchar(255)")]
    public string? EmployeeName { get; set; }

    [StringLength(255)]
    [Column(TypeName = "varchar(255)")] 
    public string? EmployeePic { get; set; }

    [StringLength(255)]
    [Column(TypeName = "varchar(20)")] 
    public string? Role { get; set; }

    [Required]
    public DateOnly HireDate { get; set; }

    [Required]
    public TimeSpan ReservationTime { get; set; }

    public double Salary { get; set; }


    // Navigation properties
    [ForeignKey("RestID")]
    public required RestaurantsModel Restaurant { get; set; }


    public ICollection<TimeSheetModel> TimeSheets { get; set; } 

    public EmployeesModel()
    {
        EmployeeID = Guid.NewGuid();
        TimeSheets = new List<TimeSheetModel>();
    }

}
