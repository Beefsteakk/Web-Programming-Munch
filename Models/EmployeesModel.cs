using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffectiveWebProg.Models;


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
    [StringLength(255)]
    [Column(TypeName = "varchar(100)")] 
    public string? Department { get; set; }

    [StringLength(255)]
    [Column(TypeName = "varchar(255)")]
    public string? Email { get; set; }

    [StringLength(255)]
    [Column(TypeName = "varchar(20)")]
    public string? PhoneNumber { get; set; }

    [Required]
    public DateOnly HireDate { get; set; }

    // Navigation properties
    [ForeignKey("RestID")]
    public RestaurantsModel? Restaurant { get; set; }

    public ICollection<TimeSheetModel> TimeSheets { get; set; } 

    public EmployeesModel()
    {
        EmployeeID = Guid.NewGuid();
        TimeSheets = new List<TimeSheetModel>();
    }

    // Optional constructor to initialize Restaurant property
    public EmployeesModel(Guid restId) : this()
    {
        RestID = restId;
    }
}
