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
        public async Task<IActionResult> AddShift([FromBody] TimeSheetModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Convert local times to UTC for storage
                // model.StartTime = DateTime.SpecifyKind(model.StartTime, DateTimeKind.Local).ToLocalTime();
                // model.EndTime = DateTime.SpecifyKind(model.EndTime, DateTimeKind.Local).ToLocalTime();

                _context.TimeSheet.Add(model);
                await _context.SaveChangesAsync();
                return Json(new { success = true, shiftId = model.SheetID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving shift: {@ShiftData}", model);
                return StatusCode(500, new { success = false, error = "An error occurred while saving the shift.", details = ex.ToString() });
            }
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

            var shiftData = new
            {
                shift.SheetID,
                shift.EmployeeID,
                EmployeeName = shift.Employees?.EmployeeName,
                Day = shift.Day.ToString("yyyy-MM-dd"),
                shift.ShiftType,
                StartTime = shift.StartTime.ToLocalTime().ToString("HH:mm"),
                EndTime = shift.EndTime.ToLocalTime().ToString("HH:mm")
            };

            return Json(shiftData);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShift([FromBody] TimeSheetModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingShift = await _context.TimeSheet.FindAsync(model.SheetID);
                if (existingShift == null)
                {
                    return NotFound();
                }

                // Convert local times to UTC for storage
                // model.StartTime = DateTime.SpecifyKind(model.StartTime, DateTimeKind.Local).ToLocalTime();
                // model.EndTime = DateTime.SpecifyKind(model.EndTime, DateTimeKind.Local).ToLocalTime();

                _context.Entry(existingShift).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating shift: {@ShiftData}", model);
                return StatusCode(500, new { success = false, error = "An error occurred while updating the shift." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteShift(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting shift: {ShiftId}", id);
                return StatusCode(500, new { success = false, error = "An error occurred while deleting the shift." });
            }
        }
    }
}