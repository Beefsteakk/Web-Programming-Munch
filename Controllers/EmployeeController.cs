using EffectiveWebProg.Data;
using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;

namespace EffectiveWebProg.Controllers
{
    public class EmployeesController : BaseController
    {
        private readonly ApplicationDbContext _db;
        // private readonly ILogger<EmployeesController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EmployeesController(ApplicationDbContext db, ILogger<EmployeesController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            // _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Employees
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter)
        {
            var restID = Guid.Parse(HttpContext.Session.GetString("SSID") ?? "");
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["DeptSortParam"] = sortOrder == "dept_asc" ? "dept_desc" : "dept_asc";

            var employees =  _db.Employees.Where(e => e.RestID == restID);

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.EmployeeName);
                    break;
                case "dept_asc":
                    employees = employees.OrderBy(e => e.Department);
                    break;
                case "dept_desc":
                    employees = employees.OrderByDescending(e => e.Department);
                    break;
                default:
                    employees = employees.OrderBy(e => e.EmployeeName); // Default to ascending by name
                    break;
            }

            return View(await employees.ToListAsync());
        }




        // GET: Employees/Create
        [HttpGet]
        public IActionResult Create()
        {
            var ssid = HttpContext.Session.GetString("SSID"); // Examplee of retrieving from session

            if (!string.IsNullOrEmpty(ssid))
            {
                var employee = new EmployeesModel { RestID = Guid.Parse(ssid) }; // Initialize with RestID from session
                // _logger.LogInformation($"ssid: meow {ssid}");

                return View(employee);
            }

            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EmployeesModel employee, IFormFile photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Handle file upload
                if (photo != null && photo.Length > 0)
                {
                    // Ensure the ~/images directory exists
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate a unique file name
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    // Save the file name to the database
                    employee.EmployeePic = fileName;
                }
                // Fetch the restaurant based on the RestID
                var restaurant = await _db.Restaurants.FindAsync(employee.RestID);
                if (restaurant == null)
                {
                    // _logger.LogError($"Restaurant not found. {employee.RestID}");
                    return BadRequest("Restaurant not found.");
                }

                // Associate the fetched restaurant with the employee
                employee.Restaurant = restaurant;

                // Save employee to database
                _db.Add(employee);
                await _db.SaveChangesAsync();

                // _logger.LogInformation("Employee added to database.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // _logger.LogError("An error occurred while creating the employee: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: Employees/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var employee = await _db.Employees.FindAsync(id);
                if (employee == null)
                {
                    // _logger.LogError($"Employee with ID {id} not found.");
                    return NotFound();
                }
                return Json(employee);
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Error fetching employee data: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: Employees/Edit
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EmployeesModel employee, IFormFile? photo)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        // _logger.LogError(error.ErrorMessage);
                    }
                }
                return BadRequest(ModelState);
            }

            try
            {
                var existingEmployee = await _db.Employees.FindAsync(employee.EmployeeID);
                if (existingEmployee == null)
                {
                    return NotFound("Employee not found.");
                }

                // Handle file upload only if a new photo is provided
                if (photo != null && photo.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    existingEmployee.EmployeePic = fileName;
                }

                existingEmployee.EmployeeName = employee.EmployeeName;
                existingEmployee.Role = employee.Role;
                existingEmployee.Department = employee.Department;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;

                _db.Update(existingEmployee);
                await _db.SaveChangesAsync();

                // _logger.LogInformation("Employee updated in database.");
                return Ok(existingEmployee);
            }
            catch (Exception ex)
            {
                // _logger.LogError("An error occurred while updating the employee: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: Employees/Delete/{id}
        [HttpDelete]
        [Route("Employees/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var employee = await _db.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound("Employee not found.");
                }

                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();

                // _logger.LogInformation($"Employee {id} deleted successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                // _logger.LogError("An error occurred while deleting the employee: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: Employees/Search
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            // _logger.LogInformation($"Search term: {searchTerm}");
            var employees = _db.Employees
                            .Where(e => e.EmployeeName.Contains(searchTerm) ||
                                        e.Role.Contains(searchTerm) ||
                                        e.Department.Contains(searchTerm))
                            .ToList();
            return View("Search", employees);
        }

    }
}
