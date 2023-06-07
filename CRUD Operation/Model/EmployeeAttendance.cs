using System;

namespace CRUD_Operation.Model
{
    public class EmployeeAttendance
    {
        public int EmployeeAttendanceId { get; set; } 
        public int EmployeeId { get; set; } 
        public DateTime AttendanceDate { get; set; }
        public int IsPresent { get; set; }
        public int IsAbsent { get; set; }
        public int IsOffday { get; set; }
        public Employee employee { get; set; }
    }
}
