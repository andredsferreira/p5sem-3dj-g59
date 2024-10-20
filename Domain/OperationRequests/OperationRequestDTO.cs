using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Staffs;


namespace DDDSample1.Domain.OperationRequests;

public class OperationRequestDTO {

    public Guid id { get; set; } = Guid.NewGuid();

    [Required]
    public PatientDTO patientDTO { get; set; }

    [Required]
    public StaffDTO staffDTO { get; set; }

    [Required]
    public OperationTypeDTO operationTypeDTO { get; set; }

    [Required]
    public string priority { get; private set; }

    [Required]
    public DateTime dateTime { get; private set; }

    [Required]
    public RequestStatus requestStatus { get; private set; }

    [Required]
    public string teste { get; private set; }

}