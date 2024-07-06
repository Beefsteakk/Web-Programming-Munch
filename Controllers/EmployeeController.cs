using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using EffectiveWebProg.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EffectiveWebProg.Controllers
{
    public class EmployeesController : BaseController
    {
        private readonly ApplicationDbContext _db;

        public EmployeesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Timesheet
        public async Task<IActionResult> Index()
        {
            // var timesheets = await _db.Timesheets.Include(t => t.User).ToListAsync();
            // if (timesheets == null || !timesheets.Any())
            // {
            //     Console.WriteLine("No timesheets found in the database.");
            // }
            return View();
        }

    }
}