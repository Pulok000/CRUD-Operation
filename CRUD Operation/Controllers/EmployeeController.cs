using CRUD_Operation.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Operation.Controllers
{
    public class EmployeeController : Controller
    {

        private ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpPut("api/employees/{id}/employeecode")]
        public async Task<IActionResult> API01(int id, [FromBody] string employeeCode)
        {
            // Check if the employee code already exists
            if (await _db.Employee.AnyAsync(e => e.EmployeeCode == employeeCode))
            {
                return BadRequest("Employee code already exists.");
            }

            // Find the employee by ID
            var employee = await _db.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Update the employee code
            employee.EmployeeCode = employeeCode;
            await _db.SaveChangesAsync(); // Use the DbContext to save changes

            return NoContent();
        }



        [HttpGet("api/employees/sortedbysalary")]
        public async Task<IActionResult> API02()
        {
            var employees = await _db.Employee.OrderByDescending(e => e.EmployeeSalary).ToListAsync();

            return Ok(employees);
        }

        [HttpGet("api/employees/absent")]
        public async Task<IActionResult> API03()
        {
            var absentEmployees = await (from e in _db.Employee
                                         join ea in _db.EmployeeAttendance on e.EmployeeId equals ea.EmployeeId
                                         where ea.IsAbsent > 0 
                                         select e).ToListAsync();

            return Ok(absentEmployees);
        }

        [HttpGet("api/employees/monthlyattendance")]
        public async Task<IActionResult> API04()
        {
            var attendanceReport = await _db.EmployeeAttendance
                .Include(a => a.employee)
                .GroupBy(a => new { a.employee.EmployeeName, a.AttendanceDate.Month })
                .Select(g => new
                {
                    EmployeeName = g.Key.EmployeeName,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                    TotalPresent = g.Sum(a => a.IsPresent),
                    TotalAbsent = g.Sum(a => a.IsAbsent),
                    TotalOffday = g.Sum(a => a.IsOffday)
                })
                .ToListAsync();

            return Ok(attendanceReport);
        }





    }
}
