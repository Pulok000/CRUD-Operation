using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_Operation.Migrations
{
    public partial class CodeFirstMigration01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttendance",
                columns: table => new
                {
                    EmployeeAttendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AttendanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPresent = table.Column<int>(type: "int", nullable: false),
                    IsAbsent = table.Column<int>(type: "int", nullable: false),
                    IsOffday = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendance", x => x.EmployeeAttendanceId);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendance_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "EmployeeCode", "EmployeeName", "EmployeeSalary" },
                values: new object[] { 502030, "EMP319", "Mehedi Hasan", 50000m });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "EmployeeCode", "EmployeeName", "EmployeeSalary" },
                values: new object[] { 502031, "EMP321", "Ashikur Rahman", 45000m });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "EmployeeCode", "EmployeeName", "EmployeeSalary" },
                values: new object[] { 502032, "EMP322", "Rakibul Islam", 52000m });

            migrationBuilder.InsertData(
                table: "EmployeeAttendance",
                columns: new[] { "EmployeeAttendanceId", "AttendanceDate", "EmployeeId", "IsAbsent", "IsOffday", "IsPresent" },
                values: new object[] { 2, new DateTime(2023, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 502030, 1, 0, 0 });

            migrationBuilder.InsertData(
                table: "EmployeeAttendance",
                columns: new[] { "EmployeeAttendanceId", "AttendanceDate", "EmployeeId", "IsAbsent", "IsOffday", "IsPresent" },
                values: new object[] { 3, new DateTime(2023, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 502031, 0, 0, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendance_EmployeeId",
                table: "EmployeeAttendance",
                column: "EmployeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAttendance");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
