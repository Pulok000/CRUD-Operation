using CRUD_Operation.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace CRUD_Operation.Data
{
        public static class DataSeeder
        {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 502030, EmployeeName = "Mehedi Hasan", EmployeeCode = "EMP319", EmployeeSalary = 50000 },
                new Employee { EmployeeId = 502031, EmployeeName = "Ashikur Rahman", EmployeeCode = "EMP321", EmployeeSalary = 45000 },
                new Employee { EmployeeId = 502032, EmployeeName = "Rakibul Islam", EmployeeCode = "EMP322", EmployeeSalary = 52000 }
            );

            modelBuilder.Entity<EmployeeAttendance>().HasData(
                new EmployeeAttendance { EmployeeAttendanceId = 1, EmployeeId = 502030, AttendanceDate = new DateTime(2023, 6, 24), IsPresent = 1, IsAbsent = 0, IsOffday = 0 },
                new EmployeeAttendance { EmployeeAttendanceId = 2, EmployeeId = 502030, AttendanceDate = new DateTime(2023, 6, 25), IsPresent = 0, IsAbsent = 1, IsOffday = 0 },
                new EmployeeAttendance { EmployeeAttendanceId = 3, EmployeeId = 502031, AttendanceDate = new DateTime(2023, 6, 25), IsPresent = 1, IsAbsent = 0, IsOffday = 0 }
            );
        }

    }
}
