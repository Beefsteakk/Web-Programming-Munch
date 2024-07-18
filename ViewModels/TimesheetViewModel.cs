using System;
using System.Collections.Generic;
using EffectiveWebProg.Models;

namespace EffectiveWebProg.ViewModels
{
    public class TimesheetViewModel
    {
        public DateTime CurrentDate { get; set; }
        public List<TimeSheetModel> Timesheets { get; set; }
        public List<EmployeesModel> Employees { get; set; }
    }
}