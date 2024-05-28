using Microsoft.EntityFrameworkCore;
namespace PrescriptionApp.Model
{
    public class PrescriptionContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }


        public PrescriptionContext()
        {
        }

        public PrescriptionContext(DbContextOptions<PrescriptionContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor);
                entity.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired();

                entity.ToTable("Doctor");

                entity.HasMany(d => d.Prescriptions)
                       .WithOne(p => p.Doctor)
                       .HasForeignKey(p => p.IdDoctor)
                       .IsRequired();
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription);
                entity.Property(e => e.IdPrescription).ValueGeneratedOnAdd();
                entity.ToTable("Prescription");

                entity.HasMany(d => d.PrescriptionMedicaments)
                       .WithOne(p => p.Prescription)
                       .HasForeignKey(p => p.IdPrescription)
                       .IsRequired();

            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient);
                entity.Property(e => e.IdPatient).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired();
                entity.ToTable("Patient");

                entity.HasMany(d => d.Prescriptions)
                       .WithOne(p => p.Patient)
                       .HasForeignKey(p => p.IdPatient)
                       .IsRequired();

            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament);
                entity.Property(e => e.IdMedicament).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
                entity.ToTable("Medicament");

                entity.HasMany(d => d.PrescriptionMedicaments)
                       .WithOne(p => p.Medicament)
                       .HasForeignKey(p => p.IdMedicament)
                       .IsRequired();


            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                entity.Property(e => e.Dose).IsRequired();
                entity.ToTable("PrescriptionMedicament");

            });

        }
    }
}
