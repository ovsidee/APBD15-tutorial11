using APBD15_tutorial11.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace APBD15_tutorial11.Services;

public interface IDbService
{
    public Task<PatientDetailsDto?> GetPatientDetailsAsync(int idPatient);
    public Task<string> AddPrescriptionAsync(AddPrescriptionRequestDto request, CancellationToken cancellationToken);
}