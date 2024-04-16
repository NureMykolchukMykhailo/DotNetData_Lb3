using Microsoft.EntityFrameworkCore;
using DotNetData_Lb3.Models;

namespace DotNetData_Lb3.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<DoctorsSchedule> Schedules => Set<DoctorsSchedule>();
        public DbSet<PatientDiscount> PatientDiscounts => Set<PatientDiscount>();

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientDiscount>()
                .HasKey(pd => new { pd.PatientId, pd.DoctorId });

            modelBuilder.Entity<DoctorsSchedule>()
                .HasKey(ds => ds.ScheduleId);
        }

    }
}
