using APBD15_tutorial11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD15_tutorial11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    protected DatabaseContext()
    {
    }
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
        {
            new Doctor { IdDoctor = 1, FirstName = "Vitalii", LastName = "Korytnyi", Email = "s31719@pjwstk.edu.pl" },
            new Doctor { IdDoctor = 2, FirstName = "Artem", LastName = "Gatsuta", Email = "artem.gatsuta@gmail.com" }
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>
        {
            new Patient { IdPatient = 1, FirstName = "Slava", LastName = "Larin", BirthDate = new DateTime(2005, 12, 6) },
            new Patient { IdPatient = 2, FirstName = "Vanya", LastName = "Semenets", BirthDate = new DateTime(2005, 10, 31) }
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>
        {
            new Prescription { IdPrescription = 1, IdPatient = 1, IdDoctor = 1, Date = new DateTime(2024, 1, 1) },
            new Prescription { IdPrescription = 2, IdPatient = 2, IdDoctor = 2, Date = new DateTime(2024, 1, 2) }
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
        {
            new Medicament { IdMedicament = 1, Name = "Amoxicillin", Description = "Antibiotic for something...", Type = "Antibiotic" },
            new Medicament { IdMedicament = 2, Name = "Ibuprofen", Description = "Pain reliever", Type = "NSAID" }
        });

        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>
        {
            new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = null, Details = "Take 2 times a day." },
            new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 2, Dose = 10, Details = "Take 1 time a day." }
        });
    }
}