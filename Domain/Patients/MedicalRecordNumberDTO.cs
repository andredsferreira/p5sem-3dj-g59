using System.ComponentModel.DataAnnotations;

namespace DDDSample1.Domain.Patients;

public class MedicalRecordNumberDTO {

    [Required]
    public string MedicalRecordNumber { get; set; }
    
}