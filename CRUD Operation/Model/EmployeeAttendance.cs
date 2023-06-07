using System;

namespace CRUD_Operation.Model
{
    public class EmployeeAttendance
    {
        public int EmployeeAttendanceId { get; set; } // Unique primary key for EmployeeAttendance
        public int EmployeeId { get; set; } // Foreign key referencing Employee
        public DateTime AttendanceDate { get; set; }
        public int IsPresent { get; set; }
        public int IsAbsent { get; set; }
        public int IsOffday { get; set; }
        public Employee employee { get; set; }
    }
}
