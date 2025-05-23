using APBD15_tutorial11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD15_tutorial11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors;
    public DbSet<Patient> Patients;
    public DbSet<Medicament> Medicaments;
    public DbSet<Prescription> Prescriptions;
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments;
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, FirstName = "Vitalii", LastName = "Korytnyi", Email = "s31719@pjwstk.edu.pl"},
            new Doctor() { IdDoctor = 2, FirstName = "Artem", LastName = "Gatsuta", Email = "artem.gatsuta@gmail.com"}
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient() {IdPatient = 1, FirstName = "Slava", LastName = "Larin", BirthDate = new DateTime(2005, 6, 25)},
            new Patient() {IdPatient = 1, FirstName = "Vanya", LastName = "Semenets", BirthDate = new DateTime(2005, 10, 31)}
        });
        
        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription() {IdPrescription = 1, IdPatient = 1, IdDoctor = 1, Date = DateTime.Now},
            new Prescription() {IdPrescription = 2, IdPatient = 1, IdDoctor = 2, Date = DateTime.Now}
        });
        
        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament() {IdMedicament = 1, Name = "Amoxicillin", Description = "Antibiotic for something...", Type = "Antibiotic"},
            new Medicament() {IdMedicament = 2, Name = "Ibuprofen", Description = "Pain reliever", Type = "NSAID"}
        });
        
        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>()
        {
            new PrescriptionMedicament() {IdMedicament = 1, IdPrescription = 1, Dose = null, Details = "Take 2 times a day."},
            new PrescriptionMedicament() {IdMedicament = 2, IdPrescription = 1, Dose = 10, Details = "Take 1 time a day."}
        });
        
    }
}