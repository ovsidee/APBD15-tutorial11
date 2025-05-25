using APBD15_tutorial11.Data;
using APBD15_tutorial11.Models;
using APBD15_tutorial11.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace APBD15_tutorial11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
     
    public async Task<string> AddPrescriptionAsync(AddPrescriptionRequestDto request, CancellationToken cancellationToken)
    {
        if (request.Medicaments.Count > 10)
            return "medicamentsCountExceeded";

        if (request.DueDate < request.Date)
            return "dueDateBeforeDate";

        // check if the doctor exists
        var doctor = await _context.Doctors.FindAsync(new object[] { request.Doctor.IdDoctor }, cancellationToken);
        if (doctor == null)
            return "doctorNotFound";

        // check if all medicaments exist
        var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicamentIds = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync(cancellationToken);

        var missingMedicaments = medicamentIds.Except(existingMedicamentIds).ToList();
        if (missingMedicaments.Any())
            return "medicamentsNotFound";

        // finding patient by ID
        Patient patient = null;
        if (request.Patient.IdPatient > 0)
        {
            patient = await _context.Patients.FindAsync(new object[] { request.Patient.IdPatient }, cancellationToken);

            if (patient != null)
            {
                if (patient.FirstName != request.Patient.FirstName ||
                    patient.LastName != request.Patient.LastName ||
                    patient.BirthDate != request.Patient.BirthDate)
                {
                    return "patientDataMismatch";
                }
            }
        }

        // create a new patient
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                BirthDate = request.Patient.BirthDate
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        // add new prescription
        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdDoctor = doctor.IdDoctor,
            IdPatient = patient.IdPatient
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync(cancellationToken);

        // add medicaments
        foreach (var med in request.Medicaments)
        {
            var prescriptionMedicament = new PrescriptionMedicament
            {
                IdPrescription = prescription.IdPrescription,
                IdMedicament = med.IdMedicament,
                Dose = med.Dose,
                Details = med.Description
            };

            _context.PrescriptionMedicaments.Add(prescriptionMedicament);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return "success";
    }
    
    public async Task<PatientDetailsDto?> GetPatientDetailsAsync(int idPatient)
    {
        var patient = await _context.Patients
            .Where(p => p.IdPatient == idPatient)
            .Select(p => new PatientDetailsDto
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                Prescriptions = _context.Prescriptions
                    .Where(pr => pr.IdPatient == p.IdPatient)
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDto
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Medicaments = _context.PrescriptionMedicaments
                            .Where(pm => pm.IdPrescription == pr.IdPrescription)
                            .Select(pm => new MedicamentDto
                            {
                                IdMedicament = pm.IdMedicament,
                                Dose = pm.Dose,
                                Description = pm.Medicament.Description
                            }).ToList(),
                        Doctor = new DoctorDto
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName,
                            Email = pr.Doctor.Email
                        }
                    }).ToList()
            }).FirstOrDefaultAsync();
        return patient;
    }
}