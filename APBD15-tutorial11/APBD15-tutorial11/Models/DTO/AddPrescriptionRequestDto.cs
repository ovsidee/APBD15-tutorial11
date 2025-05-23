namespace APBD15_tutorial11.Models.DTO;

public class AddPrescriptionRequestDto
{
    public PatientDto Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public List<AddPrescriptionMedicamentDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}