
using CRUD_Operation.Model;

using Microsoft.EntityFrameworkCore;



namespace CRUD_Operation.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ):base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<EmployeeAttendance>()
                .HasKey(ea => ea.EmployeeAttendanceId);

            modelBuilder.Entity<EmployeeAttendance>()
                .HasOne(ea => ea.employee)
                .WithOne(e => e.employeeAttendance)
                .HasForeignKey<EmployeeAttendance>(ea => ea.EmployeeId);

            DataSeeder.SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }


    }
}
