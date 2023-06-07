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
            
            if (await _db.Employee.AnyAsync(e => e.EmployeeCode == employeeCode && e.EmployeeId != id))
            {
                return BadRequest("Employee code already exists.");
            }

            
            var employee = await _db.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            
            employee.EmployeeCode = employeeCode;
            await _db.SaveChangesAsync();

            return NoContent();
        }




        [HttpGet("api/employees/sortedbysalary")]
        public async Task<IActionResult> API02()
        {
            var employees = await _db.Employee
                .OrderByDescending(e => e.EmployeeSalary)
                .Select(e => new
                {
                    e.EmployeeId,
                    e.EmployeeName,
                    e.EmployeeCode,
                    e.EmployeeSalary
                })
                .ToListAsync();

            return Ok(employees);
        }


        [HttpGet("api/employees/absent")]
        public async Task<IActionResult> API03()
        {
            var absentEmployees = await (from e in _db.Employee
                                         join ea in _db.EmployeeAttendance on e.EmployeeId equals ea.EmployeeId
                                         where ea.IsAbsent == 1
                                         select new
                                         {
                                             e.EmployeeId,
                                             e.EmployeeName,
                                             e.EmployeeCode,
                                             e.EmployeeSalary
                                         }).ToListAsync();

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
            TotalPresent = g.Count(a => a.IsPresent == 1),
            TotalAbsent = g.Count(a => a.IsAbsent == 1),
            TotalOffday = g.Count(a => a.IsOffday == 1)
        })
        .ToListAsync();

    return Ok(attendanceReport);
}





    }
}
