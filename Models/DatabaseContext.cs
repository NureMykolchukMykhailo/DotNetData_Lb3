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

            modelBuilder.Entity<TopEarningDoctor>()
                .ToView("TopEarningDoctorView").HasNoKey();

            modelBuilder.HasDbFunction(typeof(DatabaseContext)
                .GetMethod(nameof(FindTopEarningDoctors), new[] { typeof(string) }))
                .HasName("FindTopEarningDoctor");

            modelBuilder.Entity<SpentByPatient>()
                .ToView("SpentByPatient").HasNoKey();

            modelBuilder.HasDbFunction(typeof(DatabaseContext)
                .GetMethod(nameof(GetSpentByPatient), new[] { typeof(string) }));

        }

        public IQueryable<TopEarningDoctor> FindTopEarningDoctors(string date)
            => FromExpression(() => FindTopEarningDoctors(date));

        public IQueryable<SpentByPatient> GetSpentByPatient(string phone)
            => FromExpression(() => GetSpentByPatient(phone));

        [DbFunction("RemoveSubstring", "dbo")]
        public string RemoveSubstring(string input, int start, int length)
            => throw new NotImplementedException();

    }
}
