using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Operation.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public decimal EmployeeSalary { get; set; }


        public EmployeeAttendance employeeAttendance { get; set; } 
    }
}
