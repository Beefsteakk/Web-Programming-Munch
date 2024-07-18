using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using EffectiveWebProg.ViewModels;
using Microsoft.Extensions.Logging;

namespace EffectiveWebProg.Controllers
{
    public class TimesheetController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TimesheetController> _logger;

        public TimesheetController(ApplicationDbContext context, ILogger<TimesheetController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            var currentDate = date ?? DateTime.Today;
            var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(6);

            var timesheets = await _context.TimeSheet
                .Include(t => t.Employees)
                .Where(t => t.Day >= startOfWeek && t.Day <= endOfWeek)
                .ToListAsync();

            var employees = await _context.Employees.ToListAsync();

            var viewModel = new TimesheetViewModel
            {
                CurrentDate = currentDate,
                Timesheets = timesheets,
                Employees = employees
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateShift([FromBody] TimeSheetModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                model.StartTime = model.Day.Date + model.StartTime.TimeOfDay;
                model.EndTime = model.Day.Date + model.EndTime.TimeOfDay;

                // Fetch the employee based on the EmployeeID
                var employee = await _context.Employees.FindAsync(model.EmployeeID);
                if (employee == null)
                {
                    return BadRequest("Invalid EmployeeID");
                }

                // Set the Employees navigation property
                model.Employees = employee;

                if (model.SheetID == Guid.Empty)
                {
                    model.SheetID = Guid.NewGuid();
                    _context.TimeSheet.Add(model);
                }
                else
                {
                    var existingShift = await _context.TimeSheet.FindAsync(model.SheetID);
                    if (existingShift == null)
                    {
                        model.SheetID = Guid.NewGuid();
                        _context.TimeSheet.Add(model);
                    }
                    else
                    {
                        _context.Entry(existingShift).CurrentValues.SetValues(model);
                        existingShift.Employees = employee;
                    }
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true, shiftId = model.SheetID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving shift: {@ShiftData}", model);
                return StatusCode(500, new { success = false, error = "An error occurred while saving the shift.", details = ex.ToString() });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteShift(Guid id)
        {
            var shift = await _context.TimeSheet.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            _context.TimeSheet.Remove(shift);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetShift(Guid id)
        {
            var shift = await _context.TimeSheet
                .Include(t => t.Employees)
                .FirstOrDefaultAsync(t => t.SheetID == id);

            if (shift == null)
            {
                return NotFound();
            }

            return Json(shift);
        }
    }
}